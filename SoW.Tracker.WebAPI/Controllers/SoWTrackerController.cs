using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SoW.Tracker.WebAPI.Models;
using SoW.Tracker.WebAPI.Models.ViewModels;
using SoW.Tracker.WebAPI.ServiceInterface;
using SoW.Tracker.WebAPI.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoWTrackerController : ControllerBase
    {
        readonly ISoWTracker _SoWTracker = null;
        readonly LoggingConfigSection _logConfigSection = null;
        readonly IEmailCommunication _IEmail = null;
        public SoWTrackerController(ISoWTracker SoWTracker,
            IOptions<LoggingConfigSection> logConfigSection,
            IEmailCommunication iEmail)
        {
            _SoWTracker = SoWTracker;
            _logConfigSection = logConfigSection.Value;
            _IEmail = iEmail;
        }
        /// <summary>
        /// API to add New SoW Tracker record
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewSoWTracker([FromBody] SoWTrackerProfile newTracker)
        {
            ControllerResponse response = new ControllerResponse();
            try
            {
                string state = "";
                int year = 0;
                Int64 auto_inc = 0;
                if (newTracker.SoWType == "OR")
                {
                    string[] OriginalSoW = newTracker.OriginalSoWPattern.Split('-');
                    state = OriginalSoW[1];
                    year = Convert.ToInt32(OriginalSoW[3]);
                    auto_inc = Convert.ToInt64(OriginalSoW[4]);
                }
                Int64 SoW_Id = await _SoWTracker.AddNewSoWTracker(newTracker,  state,year, auto_inc);
                if(SoW_Id> 0)
                {
                    _IEmail.EmailSend(newTracker.SoWCRPattern);
                    response.data = newTracker.SoWCRPattern;
                    response.message = "SOW Record created Successfully";
                    response.messagetype = "Success";
                    response.httpStatusCode = StatusCodes.Status200OK;
                }
                else
                {
                    response.data = SoW_Id.ToString();
                    response.message = "SOW Record not created ";
                    response.messagetype = "Error";
                }                         
            }
            catch (Exception ex)
            {
                ExceptionData ed = new ExceptionData
                {
                    MethodException = ex,
                    LogConfigSection = _logConfigSection,
                    UserName = Environment.UserName,
                    MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name
                };
                return Ok(SoWExceptionHandler.LogException(ed));
            }
            return Ok(response);
        }
        /// <summary>
        /// API to add New SoW Tracker record
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSoWTracker([FromBody] UpdateSoWTracker updateTracker)
        {
            ControllerResponse response = new ControllerResponse();
            try
            {
                Int64 SoW_Id = await _SoWTracker.UpdateSoWTracker(updateTracker);
                if (SoW_Id > 0)
                {
                    response.data = "SOW Record Updated  Successfully";
                    response.message = "";
                    response.messagetype = "Success";
                    response.httpStatusCode = StatusCodes.Status200OK;
                }
                else
                {
                    response.data = SoW_Id.ToString();
                    response.message = "SOW Record Not Updated  Successfully";
                    response.messagetype = "Error";
                }
            }
            catch (Exception ex)
            {
                ExceptionData ed = new ExceptionData
                {
                    MethodException = ex,
                    LogConfigSection = _logConfigSection,
                    UserName = Environment.UserName,
                    MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name
                };
                return Ok(SoWExceptionHandler.LogException(ed));
            }
            return Ok(response);
        }

        /// <summary>
        /// API to get fiels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GenerateOriginalSOW/{State}/{Year}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GenerateOriginalSOW(string State, string Year)
        {
            ControllerResponse response = new ControllerResponse();
            try
            {
                SoWTrackerProfile result = await _SoWTracker.GetMaxSOWId(Year, State);
                string FinalSoWid = GenarateSOWId(Convert.ToString(result.OriginalSoWId+1));
                FinalSoWid = "IBM-" + State + "-" + "SOW-" + Year + "-" + FinalSoWid;
                response.data = FinalSoWid;
                response.httpStatusCode = StatusCodes.Status200OK;
            }
            catch (Exception ex)
            {
                ExceptionData ed = new ExceptionData
                {
                    MethodException = ex,
                    LogConfigSection = _logConfigSection,
                    UserName = Environment.UserName,
                    MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name
                };
                return Ok(SoWExceptionHandler.LogException(ed));
            }
            return Ok(response);
        }


        /// <summary>
        /// API to generate ChangeRequest
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GenerateSOWCR/{SOW}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GenerateSOWCR(string SOW)
        {
            ControllerResponse response = new ControllerResponse();
            try
            {
                SoWCR responsesow = new SoWCR();
                SoWTrackerProfile result = await _SoWTracker.GetMaxSOWCRId(SOW);
                string FinalSoWid = GenarateSOWCRSeries(Convert.ToString(result.SoWCRId + 1));
                responsesow.SoWCRName = SOW + "-CR" + FinalSoWid;
                responsesow.SoWCRNo = result.SoWCRId + 1;
                response.data = responsesow;
                response.httpStatusCode = StatusCodes.Status200OK;
            }
            catch (Exception ex)
            {
                ExceptionData ed = new ExceptionData
                {
                    MethodException = ex,
                    LogConfigSection = _logConfigSection,
                    UserName = Environment.UserName,
                    MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name
                };
                return Ok(SoWExceptionHandler.LogException(ed));
            }
            return Ok(response);
        }

        /// <summary>
        /// API to get SOW Tracker summary
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSoWTrackerSummary/{SowName}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSoWTrackerSummary(string SowName)
        {
            ControllerResponse response = new ControllerResponse();
            try
            {
                response.httpStatusCode = StatusCodes.Status200OK;
                SoWTrackerSummary data = await _SoWTracker.GetSoWTrackerSummary(SowName);
                response.data = data;
               
                response.messagetype = ResponseMessagetype.success.ToString();
                return Ok(response);
            }
            catch (Exception ex)
            {
                ExceptionData ed = new ExceptionData
                {
                    MethodException = ex,
                    LogConfigSection = _logConfigSection,
                    UserName = Environment.UserName,
                    MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name
                };
                return Ok(SoWExceptionHandler.LogException(ed));
            }

        }
        /// <summary>
        /// API to get all Business Units
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetOffShoreDMS")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOnOffShoreDMS()
        {
            ControllerResponse response = new ControllerResponse();
            try
            {
                response.httpStatusCode = StatusCodes.Status200OK;
                IList<OffShoreDM> data = await _SoWTracker.GetOffShoreDMS();
                response.data = data;
                response.totalRowCount = data.Count;
                response.messagetype = ResponseMessagetype.success.ToString();
                return Ok(response);
            }
            catch (Exception ex)
            {
                ExceptionData ed = new ExceptionData
                {
                    MethodException = ex,
                    LogConfigSection = _logConfigSection,
                    UserName = Environment.UserName,
                    MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name
                };
                return Ok(SoWExceptionHandler.LogException(ed));
            }
        }
        /// <summary>
        /// API to get all Business Units
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetOnShoreDMS")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOnShoreDMS()
        {
            ControllerResponse response = new ControllerResponse();
            try
            {
                response.httpStatusCode = StatusCodes.Status200OK;
                IList<OnShoreDM> data = await _SoWTracker.GetOnShoreDMS();
                response.data = data;
                response.totalRowCount = data.Count;
                response.messagetype = ResponseMessagetype.success.ToString();
                return Ok(response);
            }
            catch (Exception ex)
            {
                ExceptionData ed = new ExceptionData
                {
                    MethodException = ex,
                    LogConfigSection = _logConfigSection,
                    UserName = Environment.UserName,
                    MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name
                };
                return Ok(SoWExceptionHandler.LogException(ed));
            }
        }
        /// <summary>
        /// API to get all active years
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetYears")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetYears()
        {
            ControllerResponse response = new ControllerResponse();
            try
            {
                response.httpStatusCode = StatusCodes.Status200OK;
                IList<Years> data = await _SoWTracker.GetYears();
                response.data = data;
                response.totalRowCount = data.Count;
                response.messagetype = ResponseMessagetype.success.ToString();
                return Ok(response);
            }
            catch (Exception ex)
            {
                ExceptionData ed = new ExceptionData
                {
                    MethodException = ex,
                    LogConfigSection = _logConfigSection,
                    UserName = Environment.UserName,
                    MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name
                };
                return Ok(SoWExceptionHandler.LogException(ed));
            }
        }
        /// <summary>
        /// API to get all active years
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetOrigialSoWs")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrigialSoWs()
        {
            ControllerResponse response = new ControllerResponse();
            try
            {
                response.httpStatusCode = StatusCodes.Status200OK;
                IList<SoWOriginal> data = await _SoWTracker.GetOrigialSoWs();
                response.data = data;
                response.totalRowCount = data.Count;
                response.messagetype = ResponseMessagetype.success.ToString();
                return Ok(response);
            }
            catch (Exception ex)
            {
                ExceptionData ed = new ExceptionData
                {
                    MethodException = ex,
                    LogConfigSection = _logConfigSection,
                    UserName = Environment.UserName,
                    MethodName = System.Reflection.MethodBase.GetCurrentMethod().Name
                };
                return Ok(SoWExceptionHandler.LogException(ed));
            }
        }
        private static string GenarateSOWId(string SoWId)
        {
            string FinalSoWID = "";
            try
            {
                switch (SoWId.Length)
                {
                    case 1:
                        FinalSoWID = "0000" + SoWId;
                        break;
                    case 2:
                        FinalSoWID = "000" + SoWId;
                        break;
                    case 3:
                        FinalSoWID = "00" + SoWId;
                        break;
                    case 4:
                        FinalSoWID = "0" + SoWId;
                        break;
                    case 5:
                        FinalSoWID = SoWId;
                        break;
                    default:
                        FinalSoWID = SoWId;
                        break;
                }
                return FinalSoWID;
            }
            catch
            {
                throw;
            }

        }
        private static string GenarateSOWCRSeries(string SoWId)
        {
            string FinalSoWID = "";
            try
            {
                switch (SoWId.Length)
                {
               
                    case 1:
                        FinalSoWID = "0" + SoWId;
                        break;
                    case 2:
                        FinalSoWID =   SoWId;
                        break;
                    default:
                        FinalSoWID = SoWId;
                        break;
                }
                return FinalSoWID;
            }
            catch
            {
                throw;
            }

        }
    }
}

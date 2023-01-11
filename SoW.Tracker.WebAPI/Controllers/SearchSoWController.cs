using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SoW.Tracker.WebAPI.Models;
using SoW.Tracker.WebAPI.Models.ViewModels;
using SoW.Tracker.WebAPI.ServiceInterface;
using SoW.Tracker.WebAPI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchSoWController : ControllerBase
    {
        readonly ISeachSoW _searchSoW = null;
        readonly LoggingConfigSection _logConfigSection = null;
        readonly IEmailCommunication _IEmail = null;
        public SearchSoWController(ISeachSoW searchSoW,
            IOptions<LoggingConfigSection> logConfigSection,
            IEmailCommunication iEmail)
        {
            _searchSoW = searchSoW;
            _logConfigSection = logConfigSection.Value;
            _IEmail = iEmail;
        }

        /// <summary>
        /// API to get all Business Units
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllBusinessUnits")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBusinessUnits()
        {
            ControllerResponse response = new ControllerResponse();
            try
            {
              //  _IEmail.EmailSend();
                response.httpStatusCode = StatusCodes.Status200OK;
                IList<BusinessUnit> data = await _searchSoW.GetAllBusinessUnits();               
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
        /// API to get all CIO names
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllCIOS/{BU_ID}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCIOS(int BU_ID)
        {
            ControllerResponse response = new ControllerResponse();
            try
            {
                response.httpStatusCode = StatusCodes.Status200OK;
                IList<CIO> data = await _searchSoW.GetAllCIOs(BU_ID);
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
        /// API to get all Chubb Manager names
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllChubbManagers/{CIO_ID}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllChubbManagers(int CIO_ID)
        {
            ControllerResponse response = new ControllerResponse();
            try
            {
                response.httpStatusCode = StatusCodes.Status200OK;
                IList<ChubbManager> data = await _searchSoW.GetAlllstChubbManagers(CIO_ID);
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
        [HttpGet]
        [Route("GetFileterSowRecords/{Filter}/{Value}")]
        public async Task<IActionResult> GetFileterSowRecords(string Filter, string Value)
        {
            ControllerResponse getFileterSowRecordsResponse = new ControllerResponse();
            try
            {
                getFileterSowRecordsResponse.data = await _searchSoW.GetFileterSowRecords(Filter, Value);
                getFileterSowRecordsResponse.httpStatusCode = StatusCodes.Status200OK;
                return Ok(getFileterSowRecordsResponse);
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
        [HttpPost]
        [Route("GetDashboardSearch")]
        public async Task<IActionResult> GetAdvanceSearch([FromBody] AdvanceSearch advanceSearch)
        {
            ControllerResponse getFileterSowRecordsResponse = new ControllerResponse();
            try
            {
                IList<SearchSOW> profiledata = await _searchSoW.GetAdvanceSearchrSowRecords(advanceSearch);
                getFileterSowRecordsResponse.data = profiledata;
                getFileterSowRecordsResponse.httpStatusCode = StatusCodes.Status200OK;
                getFileterSowRecordsResponse.totalRowCount = profiledata.Count();
                return Ok(getFileterSowRecordsResponse);
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


    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SoW.Tracker.WebAPI.Models;
using SoW.Tracker.WebAPI.Models.ViewModels;
using SoW.Tracker.WebAPI.ServiceImplementation;
using SoW.Tracker.WebAPI.ServiceInterface;
using SoW.Tracker.WebAPI.Utility;

namespace SoW.Tracker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoWReviewController : ControllerBase
    {
        readonly ISoWReview _SoWReview = null;
        readonly LoggingConfigSection _logConfigSection = null;
        readonly IEmailCommunication _IEmail = null;
        public SoWReviewController(ISoWReview SoWReview,
            IOptions<LoggingConfigSection> logConfigSection,
            IEmailCommunication iEmail)
        {
            _SoWReview = SoWReview;
            _logConfigSection = logConfigSection.Value;
            _IEmail = iEmail;
        }
        /// <summary>
        /// API to get SOW Tracker summary
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSoWReview/{SowName}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSoWReviewDetails(string SowName)
        {
            ControllerResponse response = new ControllerResponse();
            try
            {
                response.httpStatusCode = StatusCodes.Status200OK;
                SoWGetReview data = await _SoWReview.GetSoWReview(SowName);
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
        /// API to add New SoW Review record
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
        public async Task<IActionResult> AddSoWReview([FromBody] SoWPostReview postReview)
        {
            ControllerResponse response = new ControllerResponse();
            try
            {
                int SoW_No = await _SoWReview.AddSoWReviewProcess(postReview);
                if (SoW_No > 0)
                { 
                    if(postReview.Status == 3)
                    {
                        _IEmail.EmailSend_TestArcReviewProcess(postReview.TestEmailID,postReview.ArcEmailID, postReview.soWPattern);
                    }
                    else if(postReview.Status == 4)
                    {
                        _IEmail.EmailSendManagerReview(postReview.OffshorePMEmail, postReview.soWPattern, "OffShore PM");
                    }
                    else if (postReview.Status == 5)
                    {
                        _IEmail.EmailSendManagerReview(postReview.OffshoreDMEmail, postReview.soWPattern, "OffShore DM");
                    }
                    else if (postReview.Status == 6)
                    {
                        _IEmail.EmailSendManagerReview(postReview.OnshoreDMEmail, postReview.soWPattern, "OnShore DM");
                    }
                    else if (postReview.Status == 7)
                    {
                        _IEmail.EmailSendManagerReview(postReview.FinalAprEmail, postReview.soWPattern,"Final Approval");
                    }



                    response.data = "Successfully Updated the status";                  
                    response.messagetype = "Success";
                    response.httpStatusCode = StatusCodes.Status200OK;
                }
                else
                {
                    response.data = SoW_No.ToString();
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

    }
}

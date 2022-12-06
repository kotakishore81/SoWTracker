using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class ManageUsersController : ControllerBase
    {
        readonly IManageUsers _manageUsers = null;
        readonly LoggingConfigSection _logConfigSection = null;
        public ManageUsersController(IManageUsers manageUsers,
            IOptions<LoggingConfigSection> logConfigSection)
        {
            _manageUsers = manageUsers;
            _logConfigSection = logConfigSection.Value;
        }

        /// <summary>
        /// API to get all IDC users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUsers()
        {
            ControllerResponse response = new ControllerResponse();
            try
            {
                response.httpStatusCode = StatusCodes.Status200OK;
                IList<Users> data = await _manageUsers.GetAllUsers();
                RecordCount count = GetUserCount(data);                             
                response.data = new UserListResponse { list = data, count = count };
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
        private static RecordCount GetUserCount(IList<Users> data)
        {
            RecordCount count = new RecordCount();
            count.TotalRecordCount = data.Count;
            return count;
        }


    }
}

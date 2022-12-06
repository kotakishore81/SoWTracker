using SoW.Tracker.WebAPI.DBModels;
using SoW.Tracker.WebAPI.Enum;
using SoW.Tracker.WebAPI.Models;
using SoW.Tracker.WebAPI.Utility.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SoW.Tracker.WebAPI.Utility.LoggerUtility.LoggerService;
using SoW.Tracker.WebAPI.Models.ViewModels;

namespace SoW.Tracker.WebAPI.Utility
{
    /// <summary>
    /// Class for handle and log the exception from API
    /// </summary>
    public static class SoWExceptionHandler
    {
        /// <summary>
        /// Log exception using logger framework.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="exeception">
        ///	String that will use as key.
        /// </param>
        /// <param name="methodName">
        ///	The value the kept in cache.
        /// </param> 
        /// <param name="userName">
        ///	user name.
        /// </param>    
        /// <returns>
        /// </returns>
        /// <history>
        ///     <change date="2019/08/02" author="Hirendra Dey">
        ///         First revision
        ///     </change>
        /// </history>
        public static ControllerResponse LogException(ExceptionData ed)
        {
            ControllerResponse exceptionControllerResponse = new ControllerResponse();
            LogEntry logEntry = new LogEntry();
            try
            {
                if (ed.MethodException is SoWApiException ||
                    ed.MethodException is SoWApiDatabaseException ||
                    ed.MethodException is SoWApiValidationException)
                {
                    logEntry.ErrorCode = ((SoWApiException)ed.MethodException).ErrorCode;
                }
                else
                {
                    logEntry.ErrorCode = BusinessEnums.ErrorCode.Other;
                }

                logEntry.EventTime = DateTime.Now;
                logEntry.MethodName = ed.MethodName;
                if (ed.MethodException.InnerException != null)
                {
                    logEntry.Message = string.Format("{0} {1}", ed.MethodException.Message, ed.MethodException.InnerException.Message);
                }
                else
                {
                    logEntry.Message = ed.MethodException.Message;
                }

                logEntry.StackTrace = ed.LogConfigSection.IsStackTraceExclude ? ed.MethodException.StackTrace : string.Empty;
                logEntry.UserName = ed.UserName;
                new Logger().Log(logEntry);
                exceptionControllerResponse.httpStatusCode = (int)ed.Status;
                exceptionControllerResponse.message = logEntry.Message;
                exceptionControllerResponse.messagetype = ResponseMessagetype.error.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return exceptionControllerResponse;
        }

        public static void LogInformation(ExceptionData ed)
        {
            try
            {
                new Logger().LogInfo(ed.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }

    /// <summary>
    /// Exception details class for passiong exception
    /// related details for logging
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ExceptionData
    {
        public Exception MethodException { get; set; }
        public LoggingConfigSection LogConfigSection { get; set; }
        public string MethodName { get; set; }
        public HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;
        public string UserName { get; set; }
        public string Information { get; set; }
    }

}

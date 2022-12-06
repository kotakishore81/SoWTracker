using NLog;
using SoW.Tracker.WebAPI.DBModels;
using SoW.Tracker.WebAPI.Utility.LoggerUtility.LoggerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.Utility.LoggerUtility.LoggerService
{
    public class Logger : ILoggerManager
    {
        static NLog.ILogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Logs UI/API error, based on the LogEntry model
        /// </summary>
        /// <param name="logEntry"></param>
        public void Log(LogEntry logEntry)
        {
            try
            {
                LogEventInfo logEventInfo = new LogEventInfo(NLog.LogLevel.Error, logger.Name, logEntry.Message);
                logEventInfo.Properties["ErrorCode"] = (int)logEntry.ErrorCode;
                logEventInfo.Properties["MethodName"] = logEntry.MethodName;
                logEventInfo.Properties["Severity"] = logEntry.Severity;
                logEventInfo.Properties["StackTrace"] = logEntry.StackTrace;
                logEventInfo.Properties["UserName"] = logEntry.UserName;
                logger.Log(logEventInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Logs UI/API informations in configured target.
        /// </summary>
        /// <param name="message">String message from UI/API</param>
        public void LogInfo(string message)
        {
            //NLog Info
            try
            {
                logger.Info(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

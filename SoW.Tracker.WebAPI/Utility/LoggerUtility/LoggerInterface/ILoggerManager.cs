using SoW.Tracker.WebAPI.DBModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.Utility.LoggerUtility.LoggerInterface
{
    public interface ILoggerManager
    {
        [ExcludeFromCodeCoverage]
        void LogInfo(string message);
        [ExcludeFromCodeCoverage]
        void Log(LogEntry logEntry);
    }
}

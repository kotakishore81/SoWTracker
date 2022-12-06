using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using static SoW.Tracker.WebAPI.Enum.BusinessEnums;

namespace SoW.Tracker.WebAPI.DBModels
{
    [ExcludeFromCodeCoverage]
    public class LogEntry
    {
        public ErrorCode ErrorCode { get; set; }
        public DateTime EventTime { get; set; }
        public string Message { get; set; }
        public string MethodName { get; set; }
        public Severity Severity { get; set; }
        public string StackTrace { get; set; }
        public string UserName { get; set; }
    }
}

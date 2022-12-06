using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.Models
{
    [ExcludeFromCodeCoverage]
    public class LoggingConfigSection
    {
        public string LogLevel { get; set; }
        public bool IsStackTraceExclude { get; set; }
    }
}

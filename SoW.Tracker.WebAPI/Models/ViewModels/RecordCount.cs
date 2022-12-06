using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.Models.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class RecordCount
    {
        public int TotalRecordCount { get; set; }
        public int AttActiveUserCount { get; set; }
        public int AttInActiveUserCount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.Models.ViewModels
{

    public class ControllerResponse
    {
        //Json Api Response 
        public dynamic data { get; set; }
        // share info regrading api request (This is message...)
        public string message { get; set; }
        // share status regrading api request (ok/bad request/internal server error)
        public int httpStatusCode { get; set; }
        // share info type regrading api request (warning/error/info/success)
        public string messagetype { get; set; }
        public int totalRowCount { get; set; }
    }
    public enum ResponseMessagetype
    {
        warn,
        error,
        info,
        success
    }
    [ExcludeFromCodeCoverage]
    public class UserListResponse
    {
        public IList<Users> list { get; set; }
        public RecordCount count { get; set; }
    }
    public class BusinessUnitListResponse
    {
        public IList<BusinessUnit> list { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.Enum
{
    public class BusinessEnums
    {
        public enum ErrorCode
        {
            Other = 100,
            DB_ConectionIssue = 200,
            VALIDATION_FirstNameEmpty = 300,
            VALIDATION_EmptyInputParameter = 301,
            API_SearchObjectNotFound = 400
        }
        public enum Severity { info, error }
    }
}

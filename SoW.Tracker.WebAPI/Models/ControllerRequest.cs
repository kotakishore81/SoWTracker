using SoW.Tracker.WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.Models
{
    public class ControllerRequest
    {
        #region Property
        public AdvanceSearch searchFields { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public Sort SortBy { get; set; }

        #endregion Property
    }

    [ExcludeFromCodeCoverage]
    public class Sort
    {
        public string Column { get; set; }
        public string Direction { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Query
    {
        public string SearchFieldName { get; set; }
        public string SearchFieldDataType { get; set; }
        public string SearchFieldOperator { get; set; }
        public string SearchFieldValue { get; set; }
    }
}

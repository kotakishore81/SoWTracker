using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.Models.ViewModels
{
    public class AdvanceSearch
    {
        public string  BusinessUnitId { get; set; }
        public string CIOId { get; set; }
        public string ChubbManagerId { get; set; }
        public string SOWNumber { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string RenewalFrequency { get; set; }
        public string IBMOnShoreDM { get; set; }
        public string IBMOffShoreDM { get; set; }
        public string OriginalSoW { get; set; }
        public string Filter { get; set; }
        public string Value { get; set; }

        //public int PageSize { get; set; }
        //public int PageNumber { get; set; }

    }
    public class SearchSOW
    {
        public Int64? sowId { get; set; }
        public string soWPattern { get; set; }
        public string businessUnit { get; set; }
        public string cio { get; set; }

        public string stage { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}

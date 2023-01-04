using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.Models.ViewModels
{
    public class AdvanceSearch
    {
        public int  BusinessUnitId { get; set; }
        public int CIOId { get; set; }
        public int ChubbManagerId { get; set; }
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
        public string OriginalSoW { get; set; }
        public string soWPattern { get; set; }
       
        public string description { get; set; }
        public string businessUnit { get; set; }
        public string cio { get; set; }
        public string chubbManager { get; set; }
        public string status { get; set; }
        public DateTime? CreationTimeLine { get; set; }
        public DateTime? UpdationTimeLine { get; set; }
        public DateTime ? ApprovalTimeLine { get; set; }
        public string RenewalFrequency { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public decimal TCV { get; set; }
        public decimal Value { get; set; }
        public string? ILC { get; set; }
        public string IBMOnShoreDM { get; set; }
        public string IBMOffShoreDM { get; set; }
        public string? ContractType1 { get; set; }
        public string? ContractType2 { get; set; }
        public string? PricingFinalized { get; set; }
        public string? DCAApprovalDone { get; set; }
        public string? ContractRegDone { get; set; }
        public string? StaffingComplete { get; set; }
        public string? PlannedGP { get; set; }
        public string? ActualGP { get; set; }
        public string? Stage { get; set; }
        public string? FromIBS { get; set; }
        public string? SalesConnect { get; set; }
        public DateTime DateSubmissionSigning { get; set; }
        public DateTime SignDateIfSigned { get; set; }
        public string? OpportunityID { get; set; }
        public string? Remarks { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}

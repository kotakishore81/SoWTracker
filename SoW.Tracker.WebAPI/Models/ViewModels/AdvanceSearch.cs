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
        public Int64? SNO { get; set; }
        public Int64? SowID { get; set; }
        public string OriginalSoW { get; set; }
        public string SoWCR { get; set; }
       
        public string Description { get; set; }
        public string BusinessUnit { get; set; }
        public string CIO { get; set; }
        public string ChubbManager { get; set; }
        public string Status { get; set; }
        public string CreationTimeLine { get; set; }
        public string UpdationTimeLine { get; set; }
        public string ApprovalTimeLine { get; set; }
        public string RenewalFrequency { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
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
        public decimal? PlannedGP { get; set; }
        public string? Stage { get; set; }
        public string? FromIBS { get; set; }
        public string? SalesConnect { get; set; }
        public string? DateSubmissionSigning { get; set; }
        public string? SignDateIfSigned { get; set; }
        public string? OpportunityID { get; set; }
        public string? Remarks { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedOn { get; set; }
    }
}

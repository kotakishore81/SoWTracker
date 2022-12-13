using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.Models.ViewModels
{
    public class SoWTrackerProfile
    {
        public string? SoWType { get; set; }
        public Int64? OriginalSoWId { get; set; }
        public string? OriginalSoWPattern { get; set; }
        public int SoWCRId { get; set; }
        public string? SoWCRPattern { get; set; }
     
        public string? Description { get; set; }
        public int BusinessUnit { get; set; }
        public int CIO { get; set; }
        public int ChubbManager { get; set; }
        public string? Status { get; set; }
        public DateTime CreationTimeLine { get; set; }
        public DateTime UpdationTimeLine { get; set; }
        public DateTime ApprovalTimeLine { get; set; }
        public string? RenewalFrequency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TCV { get; set; }
        public decimal Value { get; set; }
        public string? ILC { get; set; }
        public int IBMOnShoreDM { get; set; }
        public int IBMOffShoreDM { get; set; }
        public string? ContractType1 { get; set; }
        public string? ContractType2 { get; set; }
        public string? PricingFinalized { get; set; }
        public string? DCAApprovalDone { get; set; }
        public string ?ContractRegDone { get; set; }
        public string? StaffingComplete { get; set; }
        public string? PlannedGP { get; set; }
        public string? ActualGP { get; set; }
        public string? Stage { get; set; }
        public string ? FromIBS { get; set; }
        public string ?SalesConnect { get; set; }
        public DateTime DateSubmissionSigning { get; set; }
        public DateTime SignDateIfSigned { get; set; }
        public string ?OpportunityID { get; set; }
        public string ? Remarks { get; set; }
        public int GroupId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime UpdateOn { get; set; }
    }
    public class UpdateSoWTracker
    {
        public Int64 soWID { get; set; }
        public string? Description { get; set; }
        public int BusinessUnit { get; set; }
        public int CIO { get; set; }
        public int ChubbManager { get; set; }
        public string? Status { get; set; }
        public DateTime CreationTimeLine { get; set; }
        public DateTime UpdationTimeLine { get; set; }
        public DateTime ApprovalTimeLine { get; set; }
        public string? RenewalFrequency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TCV { get; set; }
        public decimal Value { get; set; }
        public string? ILC { get; set; }
        public int IBMOnShoreDM { get; set; }
        public int IBMOffShoreDM { get; set; }
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
        public string? UpdatedBy { get; set; }

    }
    public class OffShoreDM
    {
        public int OffshoreDMId { get; set; }
        public string? OffShoreDMName { get; set; }
    }
    public class OnShoreDM
    {
        public int OnshoreDMId { get; set; }
        public string? OnShoreDMName { get; set; }
    }
    public class Years
    {
        public int Year { get; set; }
        public string? Status { get; set; }
    }
    public class SoWCR
    {
        public int SoWCRNo { get; set; }
        public string? SoWCRName { get; set; }
    }
    public class SoWOriginal
    {
        public Int64 OriginalSoWID { get; set; }
        public string? OriginalSoWPattern { get; set; }
    }
    public class SoWTrackerSummary
    {
        public Int64? SoWID { get; set; }
        public string? originalSoWPattern { get; set; }
        public string? soWPattern { get; set; }

        public string? Description { get; set; }
        public string BusinessUnit { get; set; }
        public string CIO { get; set; }
        public string ChubbManager { get; set; }
        public string? Status { get; set; }
        public DateTime CreationTimeLine { get; set; }
        public DateTime UpdationTimeLine { get; set; }
        public DateTime ApprovalTimeLine { get; set; }
        public string? RenewalFrequency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TCV { get; set; }
        public decimal Value { get; set; }
        public string? ILC { get; set; }
        public int IBMOnShoreDM { get; set; }
        public int IBMOffShoreDM { get; set; }
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

    }

}

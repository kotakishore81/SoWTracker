namespace SoW.Tracker.WebAPI.Models.ViewModels
{
    public class SoWGetReview
    {
        public Int64 SoWID { get; set; }
        public string SoWPattern { get; set; }
        public string SoWCreatedBy { get; set; }
        public DateTime SoWCreatedOn { get; set; }
        public string TestApproved { get; set; }
        public string TestApprovedName { get; set; }
        public string ArcApproved { get; set; }
        public string ArcApprovedName { get; set; }
        public string Reason { get; set; }
        public DateTime ? TestArcSubmittedOn { get; set; }
        public string OffshorePMName { get; set; }
        public string OffshorePMEmail { get; set; }
        public DateTime? OffshorePMSubOn { get; set; }
        public string OffshoreDMName { get; set; }
        public string OffshoreDMEmail { get; set; }
        public DateTime? OffshoreDMAprOn { get; set; }
        public string OnshoreDMName { get; set; }
        public string OnshoreDMEmail { get; set; }
        public DateTime? OnshoreDMAprOn { get; set; }
        public string FinalAprName { get; set; }
        public string FinalAprEmail { get; set; }
        public DateTime? FinalAprOn { get; set; }
        public string SoWStatus { get; set; }

    }
    public class SoWPostReview
    {
        public Int64 SoWID { get; set; }
        public int Status { get; set; }
        public string TestApproved { get; set; }
        public string TestApprovedName { get; set; }
        public string ArcApproved { get; set; }
        public string ArcApprovedName { get; set; }
        public string Reason { get; set; }
        public string OffshorePMName { get; set; }
        public string OffshorePMEmail { get; set; }
        public string OffshoreDMName { get; set; }
        public string OffshoreDMEmail { get; set; }
        public string OnshoreDMName { get; set; }
        public string OnshoreDMEmail { get; set; }
        public string FinalAprName  { get; set; }
        public string FinalAprEmail { get; set; }

        public string UpdatedBy { get; set; }
    }
}

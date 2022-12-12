using Microsoft.EntityFrameworkCore;
using SoW.Tracker.WebAPI.DAL;
using SoW.Tracker.WebAPI.DBContext;
using SoW.Tracker.WebAPI.DBInterfaces;
using SoW.Tracker.WebAPI.Models.ViewModels;
using SoW.Tracker.WebAPI.ServiceInterface;

namespace SoW.Tracker.WebAPI.ServiceImplementation
{
    public class SoWReview : ISoWReview
    {
        private readonly SoWDbContext _context;

        public SoWReview(SoWDbContext context)
        {
            _context = context;
        }
        public async Task<SoWGetReview> GetSoWReview(string SoW_Name)
        {
            SoWGetReview lstSoWReview = null;
            try
            {
                await _context.LoadStoredProc(SP_SoWReview.SP_GETREVIEW)
                  .AddParam("@SOW_NM", SoW_Name)
              .ExecAsync(async r => lstSoWReview = await r.SingleAsync<SoWGetReview>());
            }
            catch
            {
                throw;
            }
            return lstSoWReview;
        }
        /// <summary>
        /// Add review process
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public async Task<int> AddSoWReviewProcess(SoWPostReview newPost)
        {
            int isInserted = 0;
            try
            {
                await _context.LoadStoredProc(SP_SoWReview.SP_POSTREVIEW)
                      .AddParam("@SoWID", newPost.SoWID)
                      .AddParam("@Status", newPost.Status)
                      .AddParam("@TestApproved", newPost.TestApproved)
                      .AddParam("@TestApprovedName", newPost.TestApprovedName)
                      .AddParam("@ArcApproved", newPost.ArcApproved)
                      .AddParam("@ArcApprovedName", newPost.ArcApprovedName)
                      .AddParam("@Reason", newPost.Reason)                    
                      .AddParam("@Off_PM", newPost.OffshorePMName)
                      .AddParam("@Off_PM_EMAIL", newPost.OffshorePMEmail)
                      .AddParam("@Off_DM", newPost.OffshoreDMName)
                      .AddParam("@Off_DM_EMAIL", newPost.OffshoreDMEmail)
                      .AddParam("@On_DM", newPost.OnshoreDMName)
                      .AddParam("@On_DM_EMAIL", newPost.OnshoreDMEmail)
                      .AddParam("@FNL_APR_NM", newPost.FinalAprName)
                      .AddParam("@FNL_APR_EMAIL", newPost.FinalAprEmail)
                      .AddParam("@UpdatedBy", newPost.UpdatedBy)
                      .AddParam("@OUT_STATUS", out IOutParam<int> returnValue)
                          .ExecNonQueryAsync();
                isInserted = returnValue.Value; ;
            }
            catch
            {
                throw;
            }
            return isInserted;
        }
    }
}

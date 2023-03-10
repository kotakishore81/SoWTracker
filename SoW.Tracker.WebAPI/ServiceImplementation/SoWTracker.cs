using SoW.Tracker.WebAPI.DAL;
using SoW.Tracker.WebAPI.DBContext;
using SoW.Tracker.WebAPI.DBInterfaces;
using SoW.Tracker.WebAPI.Models.ViewModels;
using SoW.Tracker.WebAPI.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.ServiceImplementation
{
    public class SoWTracker : ISoWTracker
    {
        private readonly SoWDbContext _context;

        public SoWTracker(SoWDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Add a new SoW Tracker
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public async Task<int> AddNewSoWTracker(SoWTrackerProfile newSoW, string state,int year, Int64 auto_inc)
        {
            int isInserted = 0;
            try
            {
                await _context.LoadStoredProc(SP_SoWTracker.SP_ADDNEWSOWTRACKER)
                      .AddParam("@SOW_TYPE", newSoW.SoWType)
                      .AddParam("@State", state)
                      .AddParam("@Year", year)
                      .AddParam("@ORIGINAL_YEAR_AUTO_ID", auto_inc)
                      .AddParam("@ORIGINAL_SOW_PATTERN", newSoW.OriginalSoWPattern)
                      .AddParam("@SOW_CR_NO", newSoW.SoWCRId)
                      .AddParam("@SOW_NM", newSoW.SoWCRPattern.Trim())
                      .AddParam("@SOW_DESC", newSoW.Description.Trim())
                      .AddParam("@SOW_BU", newSoW.BusinessUnit)
                      .AddParam("@SOW_CIO", newSoW.CIO)
                      .AddParam("@SOW_CM", newSoW.ChubbManager)
                      .AddParam("@SOW_STATUS", newSoW.Status.Trim())
                      .AddParam("@SOW_CRT_TML", newSoW.CreationTimeLine)
                      .AddParam("@SOW_UPT_TML", newSoW.UpdationTimeLine)
                      .AddParam("@SOW_APR_TML", newSoW.ApprovalTimeLine)
                      .AddParam("@SOW_RN_FR", newSoW.RenewalFrequency.Trim())
                      .AddParam("@SOW_ST_DT", newSoW.StartDate)
                      .AddParam("@SOW_ED_DT", newSoW.EndDate)
                      .AddParam("@SOW_TCV", newSoW.TCV)
                      .AddParam("@SOW_VALUE", newSoW.Value)
                      .AddParam("@SOW_ILC", newSoW.ILC.Trim())
                      .AddParam("@SOW_IBM_ON_DM", newSoW.IBMOnShoreDM)
                      .AddParam("@SOW_IBM_OFF_DM", newSoW.IBMOffShoreDM)
                      .AddParam("@SOW_CNT_TY_ONE", newSoW.ContractType1.Trim())
                      .AddParam("@SOW_CNT_TY_TWO", newSoW.ContractType2.Trim())
                      .AddParam("@SOW_PRC_FINAL", newSoW.PricingFinalized.Trim())
                      .AddParam("@SOW_DCA_APR_DN", newSoW.DCAApprovalDone.Trim())
                      .AddParam("@SOW_CNT_REG_DN", newSoW.ContractRegDone.Trim())
                      .AddParam("@SOW_STF_CM", newSoW.StaffingComplete.Trim())
                      .AddParam("@SOW_PLN_GP", newSoW.PlannedGP)
                      .AddParam("@SOW_STAGE", newSoW.Stage.Trim())
                      .AddParam("@SOW_FRM_IBS", newSoW.FromIBS.Trim())
                      .AddParam("@SOW_SALES_CNT", newSoW.SalesConnect.Trim())
                      .AddParam("@SOW_DT_SUB_SN", newSoW.DateSubmissionSigning)
                      .AddParam("@SOW_SN_DT_IF_SN", newSoW.SignDateIfSigned)
                      .AddParam("@SOW_REMARKS", newSoW.Remarks)
                      .AddParam("@SOW_CREATEDBY", newSoW.CreatedBy)
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

        /// <summary>
        /// Add a new SoW Tracker
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public async Task<int> UpdateSoWTracker(UpdateSoWTracker updateSoW)
        {
            int isInserted = 0;
            try
            {
                await _context.LoadStoredProc(SP_SoWTracker.SP_UPDATESOWTRACKER)
                      .AddParam("@SOW_ID", updateSoW.soWID)
                      .AddParam("@SOW_DESC", updateSoW.Description.Trim())
                      .AddParam("@SOW_BU", updateSoW.BusinessUnit)
                      .AddParam("@SOW_CIO", updateSoW.CIO)
                      .AddParam("@SOW_CM", updateSoW.ChubbManager)
                      .AddParam("@SOW_STATUS", updateSoW.Status.Trim())
                      .AddParam("@SOW_CRT_TML", updateSoW.CreationTimeLine)
                      .AddParam("@SOW_UPT_TML", updateSoW.UpdationTimeLine)
                      .AddParam("@SOW_APR_TML", updateSoW.ApprovalTimeLine)
                      .AddParam("@SOW_RN_FR", updateSoW.RenewalFrequency.Trim())
                      .AddParam("@SOW_ST_DT", updateSoW.StartDate)
                      .AddParam("@SOW_ED_DT", updateSoW.EndDate)
                      .AddParam("@SOW_TCV", updateSoW.TCV)
                      .AddParam("@SOW_VALUE", updateSoW.Value)
                      .AddParam("@SOW_ILC", updateSoW.ILC.Trim())
                      .AddParam("@SOW_IBM_ON_DM", updateSoW.IBMOnShoreDM)
                      .AddParam("@SOW_IBM_OFF_DM", updateSoW.IBMOffShoreDM)
                      .AddParam("@SOW_CNT_TY_ONE", updateSoW.ContractType1.Trim())
                      .AddParam("@SOW_CNT_TY_TWO", updateSoW.ContractType2.Trim())
                      .AddParam("@SOW_PRC_FINAL", updateSoW.PricingFinalized.Trim())
                      .AddParam("@SOW_DCA_APR_DN", updateSoW.DCAApprovalDone.Trim())
                      .AddParam("@SOW_CNT_REG_DN", updateSoW.ContractRegDone.Trim())
                      .AddParam("@SOW_STF_CM", updateSoW.StaffingComplete.Trim())
                      .AddParam("@SOW_PLN_GP", updateSoW.PlannedGP)
                      .AddParam("@SOW_STAGE", updateSoW.Stage.Trim())
                      .AddParam("@SOW_FRM_IBS", updateSoW.FromIBS.Trim())
                      .AddParam("@SOW_SALES_CNT", updateSoW.SalesConnect.Trim())
                      .AddParam("@SOW_DT_SUB_SN", updateSoW.DateSubmissionSigning)
                      .AddParam("@SOW_SN_DT_IF_SN", updateSoW.SignDateIfSigned)
                      .AddParam("@SOW_REMARKS", updateSoW.Remarks)
                      .AddParam("@UPDATED_BY", updateSoW.UpdatedBy)
                      .AddParam("@OUT_STATUS", out IOutParam<int> returnValue)
                          .ExecNonQueryAsync();
                isInserted = returnValue.Value; 
            }
            catch
            {
                throw;
            }
            return isInserted;
        }

        /// <summary>
        /// Add a new SoW Tracker
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>

        public async Task<IList<OffShoreDM>> GetOffShoreDMS()
        {
            List<OffShoreDM> lsOffShoreDMS = null;
            try
            {
                await _context.LoadStoredProc(SP_SoWTracker.SP_OFFSHOREDMS)
                .ExecAsync(async r => lsOffShoreDMS = await r.ToListAsync<OffShoreDM>());
            }
            catch
            {
                throw;
            }
            return lsOffShoreDMS;
        }
        public async Task<IList<OnShoreDM>> GetOnShoreDMS()
        {
            List<OnShoreDM> lsOnShoreDMS = null;
            try
            {
                await _context.LoadStoredProc(SP_SoWTracker.SP_ONSHOREDMS)
                .ExecAsync(async r => lsOnShoreDMS = await r.ToListAsync<OnShoreDM>());
            }
            catch
            {
                throw;
            }
            return lsOnShoreDMS;
        }
        public async Task<IList<Years>> GetYears()
        {
            List<Years> lsYears = null;
            try
            {
                await _context.LoadStoredProc(SP_SoWTracker.SP_YEARS)
                .ExecAsync(async r => lsYears = await r.ToListAsync<Years>());
            }
            catch
            {
                throw;
            }
            return lsYears;
        }
        public async Task<IList<SoWOriginal>> GetOrigialSoWs()
        {
            List<SoWOriginal> lsSoWriginal = null;
            try
            {
                await _context.LoadStoredProc(SP_SoWTracker.SP_ORIGINALSOWS)
                .ExecAsync(async r => lsSoWriginal = await r.ToListAsync<SoWOriginal>());
            }
            catch
            {
                throw;
            }
            return lsSoWriginal;
        }

        public async Task<SoWTrackerProfile> GetMaxSOWId(string Year, string State)
        {
            SoWTrackerProfile? result = null;
            try
            {
                await _context.LoadStoredProc(SP_SoWTracker.SP_MAXORIGINALID)
                    .AddParam("@Year", Year)
                    .AddParam("@State", State)
                    .ExecAsync(async r => result = await r.FirstOrDefaultAsync<SoWTrackerProfile>());
            }
            catch 
            {
                throw;
            }
            return result;
        }
        public async Task<SoWTrackerProfile> GetMaxSOWCRId(string OrignalSOW)
        {
            SoWTrackerProfile result = null;
            try
            {
                await _context.LoadStoredProc(SP_SoWTracker.SP_MAXSOWCRID)
                     .AddParam("@OriginalSOW", OrignalSOW)
                    .ExecAsync(async r => result = await r.FirstOrDefaultAsync<SoWTrackerProfile>());
            }
            catch
            {
                throw;
            }
            return result;
        }
        public async Task<SoWTrackerSummary> GetSoWTrackerSummary(string SoW_Name)
        {
            SoWTrackerSummary lstSoWTracker = null;
            try
            {
                await _context.LoadStoredProc(SP_SoWTracker.SP_SOWTRACKERSUMMARY)
                  .AddParam("@SOW_NM", SoW_Name)
              .ExecAsync(async r => lstSoWTracker = await r.SingleAsync<SoWTrackerSummary>());
            }
            catch
            {
                throw;
            }
            return lstSoWTracker;
        }
    }
}

using SoW.Tracker.WebAPI.DAL;
using SoW.Tracker.WebAPI.DBContext;
using SoW.Tracker.WebAPI.Models;
using SoW.Tracker.WebAPI.Models.ViewModels;
using SoW.Tracker.WebAPI.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.ServiceImplementation
{
    public class SearchSoW : ISeachSoW
    {
        private readonly SoWDbContext _context;

        public SearchSoW(SoWDbContext context)
        {
            _context = context;

        }
        public async Task<IList<BusinessUnit>> GetAllBusinessUnits()
        {
            List<BusinessUnit> lstBusinessUnit = null;
            try
            {
                await _context.LoadStoredProc(SP_SearchSoW.SP_GETALLBUSINESSUNITS)
                .ExecAsync(async r => lstBusinessUnit = await r.ToListAsync<BusinessUnit>());
            }
            catch
            {
                throw;
            }
            return lstBusinessUnit;
        }
        public async Task<IList<CIO>> GetAllCIOs(int BU_ID)
        {
            List<CIO> lstCIO = null;
            try
            {
                await _context.LoadStoredProc(SP_SearchSoW.SP_GETALLCIOS)
                    .AddParam("@BU_ID", BU_ID)
                .ExecAsync(async r => lstCIO = await r.ToListAsync<CIO>());
            }
            catch
            {
                throw;
            }
            return lstCIO;
        }
        public async Task<IList<ChubbManager>> GetAlllstChubbManagers(int CIO_ID)
        {
            List<ChubbManager> lstChubbManager = null;
            try
            {
                await _context.LoadStoredProc(SP_SearchSoW.SP_GETALLCHUBBMANAGERS)
                    .AddParam("@CIO_ID", CIO_ID)
                .ExecAsync(async r => lstChubbManager = await r.ToListAsync<ChubbManager>());
            }
            catch
            {
                throw;
            }
            return lstChubbManager;
        }

        public async Task<IList<SoWTrackerProfile>> GetFileterSowRecords(string Filter, string Value)
        {
            List<SoWTrackerProfile> lstSoWTracker = null;
            try
            {
                await _context.LoadStoredProc(SP_SearchSoW.SP_GETFILTERSOWRECORDS)
                .AddParam("@FilterCondition", this.GenerateSearhFilter(Filter, Value))
                .ExecAsync(async r => lstSoWTracker = await r.ToListAsync<SoWTrackerProfile>());
            }
            catch (Exception)
            {
                throw;
            }
            return lstSoWTracker;
        }
        public async Task<IList<SoWTrackerProfile>> GetAdvanceSearchrSowRecords(AdvanceSearch advanceSearch)
        {
                List<SoWTrackerProfile> lstSoWTracker = null;
            try
            {
                await _context.LoadStoredProc(SP_SearchSoW.SP_GETFILTERSOWRECORDS)
                .AddParam("@FilterCondition", this.GenerateAdvacneSearhFilter(advanceSearch))
                .ExecAsync(async r => lstSoWTracker = await r.ToListAsync<SoWTrackerProfile>());
            }
            catch (Exception)
            {
                throw;
            }
            return lstSoWTracker;
        }
        private string GenerateSearhFilter(string Filter, string Value)
        {
            string strFinalFilterClause = string.Empty;
            try
            {
                switch (Filter)
                {
                    case "SpecificRequest":
                        strFinalFilterClause = " AND SOW_NM =" + "'" + Value + "'";
                        break;
                    case "AssingedYou":
                        strFinalFilterClause = " AND  CREATED_BY =" + "'" + Value + "'";
                        break;
                    case "AssignedGroup":
                        strFinalFilterClause = " AND GRP_ID =" + "'" + Value + "'";
                        break;
                    case "All":
                        strFinalFilterClause = "";
                        break;
                    default:
                        strFinalFilterClause = "";
                        break;

                }
                return strFinalFilterClause;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private string GenerateAdvacneSearhFilter(AdvanceSearch advanceSearch)
        {
            string strFinalFilterClause = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(advanceSearch.BusinessUnitId)))
                {
                    strFinalFilterClause = strFinalFilterClause + " AND SOW_BU =" + "'" + advanceSearch.BusinessUnitId + "'";
                }
                if (!string.IsNullOrEmpty(Convert.ToString(advanceSearch.CIOId)))
                {
                    strFinalFilterClause = strFinalFilterClause + " AND SOW_CIO =" + "'" + advanceSearch.CIOId + "'";
                }
                if (!string.IsNullOrEmpty(Convert.ToString(advanceSearch.ChubbManagerId)))
                {
                    strFinalFilterClause = strFinalFilterClause+ " AND SOW_CM =" + "'" + advanceSearch.ChubbManagerId + "'";
                }
                if (!string.IsNullOrEmpty(Convert.ToString(advanceSearch.SOWNumber)))
                {
                    strFinalFilterClause = strFinalFilterClause + " AND SOW_NM =" + "'" + advanceSearch.SOWNumber + "'";
                }
                if (!string.IsNullOrEmpty(advanceSearch.StartDate))
                {
                    strFinalFilterClause = strFinalFilterClause + " AND cast(SOW_ST_DT as date) =" + "'" +  advanceSearch.StartDate + "'";
                }
                if (!string.IsNullOrEmpty(advanceSearch.EndDate))
                {
                    strFinalFilterClause = strFinalFilterClause+ " AND cast(SOW_ED_DT as date) =" + "'" + advanceSearch.EndDate + "'";
                }
                if (!string.IsNullOrEmpty(advanceSearch.RenewalFrequency))
                {              
                    strFinalFilterClause = strFinalFilterClause + " AND  SOW_RN_FR =" + "'" + advanceSearch.RenewalFrequency + "'";             
                }
                if (!string.IsNullOrEmpty(advanceSearch.IBMOnShoreDM))
                {        
                    strFinalFilterClause = strFinalFilterClause + " AND SOW_IBM_ON_DM =" + "'" + advanceSearch.IBMOnShoreDM + "'";
                }
                if (!string.IsNullOrEmpty(advanceSearch.IBMOffShoreDM))
                {         
                    strFinalFilterClause = strFinalFilterClause + " AND SOW_IBM_OFF_DM =" + "'" + advanceSearch.IBMOffShoreDM + "'";
                }
                if (!string.IsNullOrEmpty(advanceSearch.OriginalSoW))
                {                  
                    strFinalFilterClause = strFinalFilterClause + " AND  ORIGINAL_SOW_ID =" + "'" + advanceSearch.OriginalSoW + "'";
                }
                strFinalFilterClause = strFinalFilterClause + this.GenerateSearhFilter(advanceSearch.Filter, advanceSearch.Value);

                strFinalFilterClause = "WHERE" + strFinalFilterClause.Remove(0, 4);
            }
            catch (Exception)
            {
                throw;
            }
            return strFinalFilterClause;
        }
    }
}

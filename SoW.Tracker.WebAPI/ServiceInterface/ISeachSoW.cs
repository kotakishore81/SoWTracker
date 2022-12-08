using SoW.Tracker.WebAPI.Models;
using SoW.Tracker.WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SoW.Tracker.WebAPI.ServiceInterface
{
   public interface ISeachSoW
    {
        public Task<IList<BusinessUnit>> GetAllBusinessUnits();
        public Task<IList<CIO>> GetAllCIOs(int BU_ID);
        public Task<IList<ChubbManager>> GetAlllstChubbManagers(int CIO_ID);
        Task<IList<SoWTrackerProfile>> GetFileterSowRecords(string Filter, string Value);
        Task<IList<SearchSOW>> GetAdvanceSearchrSowRecords(AdvanceSearch advanceSearch);
    }
}

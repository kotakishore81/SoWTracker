using SoW.Tracker.WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.ServiceInterface
{
   public interface ISoWTracker
    {
        Task<int> AddNewSoWTracker(SoWTrackerProfile newSoW);
        Task<IList<SoWTrackerProfile>> GetSoWTrackerSummary(string SoWID);
        Task<SoWTrackerProfile> GetMaxSOWId();
        Task<SoWTrackerProfile> GetMaxSOWCRId(string OrignalSOW);
        Task<IList<OffShoreDM>> GetOffShoreDMS();
        Task<IList<OnShoreDM>> GetOnShoreDMS();
    }
}

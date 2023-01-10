using SoW.Tracker.WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.ServiceInterface
{
   public interface ISoWTracker
    {
        Task<int> AddNewSoWTracker(SoWTrackerProfile newSoW, string state, int year, Int64 auto_inc);
        Task<int> UpdateSoWTracker(UpdateSoWTracker updateSoW);
        Task<SoWTrackerSummary> GetSoWTrackerSummary(string SoW_Name);
        Task<SoWTrackerProfile> GetMaxSOWId(string Year, string State);
        Task<SoWTrackerProfile> GetMaxSOWCRId(string OrignalSOW);
        Task<IList<OffShoreDM>> GetOffShoreDMS();
        Task<IList<OnShoreDM>> GetOnShoreDMS();
        Task<IList<Years>> GetYears();
        Task<IList<SoWOriginal>> GetOrigialSoWs();
    }
}

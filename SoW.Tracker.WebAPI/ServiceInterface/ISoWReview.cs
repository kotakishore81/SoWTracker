using SoW.Tracker.WebAPI.Models.ViewModels;

namespace SoW.Tracker.WebAPI.ServiceInterface
{
    public interface ISoWReview
    {
        Task<SoWGetReview> GetSoWReview(string SOWName);
        Task<int> AddSoWReviewProcess(SoWPostReview newPost);

    }
}

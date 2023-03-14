using Ecm.Models;

namespace Ecm.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewerById(int id);
        ICollection<Review> GetReviewByReviewer(int reviewerId);
        bool ReviewerExists(int reviewerId);
    }
}

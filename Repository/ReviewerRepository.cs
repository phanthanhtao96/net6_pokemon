using Ecm.Data;
using Ecm.Interfaces;
using Ecm.Models;

namespace Ecm.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;

        public ReviewerRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Review> GetReviewByReviewer(int reviewerId)
        {
            return _context.Reviews.Where(re => re.Reviewer.Id == reviewerId).ToList();
        }

        public Reviewer GetReviewerById(int reviewerId)
        {
            return _context.Reviewers.Where(re => re.Id == reviewerId).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _context.Reviewers.Any(re => re.Id == reviewerId);
        }
    }
}

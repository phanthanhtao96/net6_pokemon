using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Ecm.Dto;
using Ecm.Interfaces;
using Ecm.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : ControllerBase
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewer()
        {
            var reviewer = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());

            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            return Ok(reviewer);
        }

        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewerById(int reviewerId) 
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId)) return NotFound();

             var reviewer = _mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewerById(reviewerId));

            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            return Ok(reviewer);

        }

        [HttpGet("{reviewerId}/reviews")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId)) return NotFound();

            var reviews = _mapper.Map<List<ReviewDto>>(_reviewerRepository.GetReviewByReviewer(reviewerId));

            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReviewer([FromBody] ReviewerDto reviewer)
        {
            if (reviewer == null) return BadRequest(ModelState);

            var checkReviewer = _reviewerRepository.GetReviewers().Where(re => re.LastName.Trim().ToUpper() == reviewer.LastName.ToUpper()).FirstOrDefault();

            if (checkReviewer != null)
            {
                ModelState.AddModelError("", "Reviewer exists");
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPut("{reviewerId}")]
        public IActionResult UpdateReviewer(int reviewerId, [FromBody] ReviewerDto reviewerBody)
        {
            if (reviewerBody == null)
            {
                ModelState.AddModelError("", "Data body is Empty, Please input!!!");
                return StatusCode(404, ModelState);
            }
            if (reviewerId != reviewerBody.Id)
            {
                ModelState.AddModelError("", "Something went wrong reviewer");
                return StatusCode(502, ModelState);
            }

            if (!_reviewerRepository.ReviewerExists(reviewerId)) return NotFound();
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var reviewerMap = _mapper.Map<Reviewer>(reviewerBody);
            if (!_reviewerRepository.UpdateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong reviewer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}

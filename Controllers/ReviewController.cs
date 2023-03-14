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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewsDto>>(_reviewRepository.GetReviews());

            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewById(int reviewId) 
        {
            if (!_reviewRepository.ReviewExists(reviewId)) return NotFound();
            
            var review = _mapper.Map<Review>(_reviewRepository.GetReviewById(reviewId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet("pokemon/{pokemonId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewForPokemon(int pokemonId)
        {
            var reviews = _mapper.Map<List<ReviewsDto>>(_reviewRepository.GetReviewOfPokemon(pokemonId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(reviews);
            {

            }
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromBody] ReviewsDto reviewBody)
        {
            if (reviewBody == null) return BadRequest(ModelState);

            var review = _reviewRepository.GetReviews().Where(re => re.Title.Trim().ToUpper() == reviewBody.Title.TrimEnd().ToUpper()).FirstOrDefault();

            if (review != null)
            {
                ModelState.AddModelError("", "Review alreay exists");
                return StatusCode(422, ModelState);
            }

            if (ModelState.IsValid) return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(reviewBody);

            if (!_reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}

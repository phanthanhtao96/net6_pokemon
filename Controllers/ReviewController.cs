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
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper, IPokemonRepository pokemonRepository, IReviewerRepository reviewerRepository)
        {
            _reviewRepository = reviewRepository;
            _pokemonRepository = pokemonRepository;
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

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
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewOfPokemon(pokemonId));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(reviews);
            {

            }
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int pokemonId, [FromBody] ReviewDto reviewBody)
        {
            if (reviewBody == null) return BadRequest(ModelState);

            var review = _reviewRepository.GetReviews().Where(re => re.Title.Trim().ToUpper() == reviewBody.Title.TrimEnd().ToUpper()).FirstOrDefault();

            // Check record ItemReview in Database
            if (review != null)
            {
                ModelState.AddModelError("", "Review alreay exists");
                return StatusCode(422, ModelState);
            }

            if (ModelState.IsValid) return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(reviewBody);
            reviewMap.Pokemon = _pokemonRepository.GetPokemonbyId(pokemonId);
            reviewMap.Reviewer = _reviewerRepository.GetReviewerById(reviewerId);

            if (!_reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int reviewId, [FromBody] ReviewDto reviewBody)
        {
            if (reviewBody == null) return BadRequest(ModelState);
            if (reviewId != reviewBody.Id) return BadRequest(ModelState);

            if (!_reviewRepository.ReviewExists(reviewId)) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var reviewMap = _mapper.Map<Review>(reviewBody);

            if (!_reviewRepository.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong review");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }
    }
}

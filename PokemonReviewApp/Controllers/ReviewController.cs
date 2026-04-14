using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dtos;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewerRepository _reviewerRepository;

        public ReviewController(IReviewRepository reviewRepository,
            IMapper mapper,
            IPokemonRepository pokemonRepository,
            IReviewerRepository reviewerRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _pokemonRepository = pokemonRepository;
            _reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public IActionResult GetReviews()
        {
            var reviews = _reviewRepository.GetReviews();
            var reviewDtos = _mapper.Map<List<ReviewDto>>(reviews);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewDtos);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(ReviewDto))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var review = _reviewRepository.GetReview(reviewId);
            var reviewDto = _mapper.Map<ReviewDto>(review);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewDto);
        }

        [HttpGet("pokemon/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsOfAPokemon(int pokeId)
        {
          
            if (!_pokemonRepository.PokemonExists(pokeId))
                return NotFound();

            var reviews = _reviewRepository.GetReviewsOfAPokemon(pokeId);
            var reviewDtos = _mapper.Map<List<ReviewDto>>(reviews);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewDtos);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        // Query ကနေ reviewerId နဲ့ pokeId ကိုပါ ယူဖို့ လိုပါတယ်
        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int pokeId, [FromBody] ReviewDto reviewCreate)
        {
            if (reviewCreate == null)
                return BadRequest(ModelState);

            // နာမည်တူ စစ်တဲ့ logic (မင်းရေးထားတာ မှန်ပါတယ်)
            var reviews = _reviewRepository.GetReviews()
                .Where(c => c.Title.Trim().ToUpper() == reviewCreate.Title.Trim().ToUpper()).FirstOrDefault();

            if (reviews != null)
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(reviewCreate);

            // --- ဒီအပိုင်းလေး ထပ်ထည့်ပေးရပါမယ် ---
            // Reviewer နဲ့ Pokemon ကို Database ထဲကနေ ရှာပြီး Review နဲ့ ချိတ်ပေးတာပါ
            reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerId);
            reviewMap.Pokemon = _pokemonRepository.GetPokemon(pokeId);

            if (reviewMap.Reviewer == null || reviewMap.Pokemon == null)
            {
                ModelState.AddModelError("", "Reviewer or Pokemon not found");
                return StatusCode(404, ModelState);
            }
            // ----------------------------------

            if (!_reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{reviewId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int reviewId, [FromBody] ReviewDto updatedReview)
        {
            if (updatedReview == null)
                return BadRequest(ModelState);

            if (reviewId != updatedReview.Id)
                return BadRequest(ModelState);

            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var reviewMap = _mapper.Map<Review>(updatedReview);

            if (!_reviewRepository.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong updating review");
                return StatusCode(500, ModelState);
            }

            return Ok("Updated successfully");
        }

        [HttpDelete("review/{reviewId}")]
        public IActionResult DeleteReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var reviewToDelete = _reviewRepository.GetReview(reviewId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReview(reviewToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting review");
                return StatusCode(500, ModelState);
            }

            return Ok("Deleted successfully");
        }
        [HttpDelete("reviewer/{reviewerId}")]
        public IActionResult DeleteReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
                return NotFound();

            var reviewsToDelete = _reviewerRepository.GetReviewsByReviewer(reviewerId).ToList();
            var reviewerToDelete = _reviewerRepository.GetReviewer(reviewerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

         
            if (!_reviewRepository.DeleteReviews(reviewsToDelete))
            {
                ModelState.AddModelError("", "error deleting reviews");
                return StatusCode(500, ModelState);
            }

          
            if (!_reviewerRepository.DeleteReviewer(reviewerToDelete))
            {
                ModelState.AddModelError("", "error deleting reviewer");
                return StatusCode(500, ModelState);
            }

            return Ok("Reviewer and all their reviews deleted successfully");
        }


    }
}
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
    public class CountryController : Controller
    {
        private readonly ICountryRepository countryRepository;
        private readonly IMapper mapper;
        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            this.countryRepository = countryRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CountryDto>))]
        public IActionResult GetCountries()
        {
            var countries = countryRepository.GetCountries();

            var countryDtos = mapper.Map<List<CountryDto>>(countries);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countryDtos);
        }

        [HttpGet("{countryId:int}")]
        [ProducesResponseType(200, Type = typeof(CountryDto))]
        [ProducesResponseType(404)]
        public IActionResult GetCountry(int countryId)
        {
            if (!countryRepository.CountryExitst(countryId))
                return NotFound();

            var country = countryRepository.GetCountry(countryId);


            var countryDto = mapper.Map<CountryDto>(country);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countryDto);
        }
        [HttpGet("owners/{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200,Type=typeof(CountryDto))]
        public IActionResult GetCountryOfAnOwner(int ownerId)
        {
            var country=mapper.Map<CountryDto>(countryRepository.GetCountryByOwner(ownerId));
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(country);
        }
        [HttpGet("owners/fromCountry/{countryId}")] 
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetOwnersByCountry(int countryId)
        {
          
            if (!countryRepository.CountryExitst(countryId))
            {
                return NotFound();
            }

           
            var owners = countryRepository.GetOwnersFromCountry(countryId);

           
            var ownerDtos = mapper.Map<List<OwnerDto>>(owners);

         
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(ownerDtos);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry([FromBody] CountryDto countryCreate)
        {
            if (countryCreate == null)
                return BadRequest(ModelState);

            var country = countryRepository.GetCountries()
                .Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("", "Country already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryMap = mapper.Map<Country>(countryCreate);

            if (!countryRepository.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        // --- Update Country ---
        [HttpPut("{countryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCountry(int countryId, [FromBody] CountryDto updatedCountry)
        {
            if (updatedCountry == null)
                return BadRequest(ModelState);

            if (countryId != updatedCountry.Id)
                return BadRequest(ModelState);

            if (!countryRepository.CountryExitst(countryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var countryMap = mapper.Map<Country>(updatedCountry);

            if (!countryRepository.UpdateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating country");
                return StatusCode(500, ModelState);
            }

            return Ok("Updated successfully");
        }

        [HttpDelete("{countryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCountry(int countryId)
        {
           
            if (!countryRepository.CountryExitst(countryId))
            {
                return NotFound();
            }

            var countryToDelete = countryRepository.GetCountry(countryId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // ၂။ ဖျက်မယ်
            if (!countryRepository.DeleteCountry(countryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting country");
                return StatusCode(500, ModelState);
            }

            return Ok("Deleted successfully");
        }
    }
}

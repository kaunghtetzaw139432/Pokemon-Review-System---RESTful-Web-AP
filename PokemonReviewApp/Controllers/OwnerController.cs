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
    public class OwnerController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper,IPokemonRepository pokemonRepository, ICountryRepository countryRepository)
        {
            _ownerRepository = ownerRepository;
            _pokemonRepository = pokemonRepository;

            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDto>))]
        public IActionResult GetOwners()
        {
            var owners = _ownerRepository.GetOwners();
            var ownerDtos = _mapper.Map<List<OwnerDto>>(owners);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ownerDtos);
        }

        [HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(OwnerDto))]
        [ProducesResponseType(400)]
        public IActionResult GetOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();

            var owner = _ownerRepository.GetOwner(ownerId);
            var ownerDto = _mapper.Map<OwnerDto>(owner);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ownerDto);
        }

        [HttpGet("{ownerId}/pokemon")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PokemonDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
            {
                return NotFound();
            }

            var pokemons = _ownerRepository.GetPokemonByOwner(ownerId);
            var pokemonDtos = _mapper.Map<List<PokemonDto>>(pokemons);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemonDtos);
        }

        [HttpGet("pokemon/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OwnerDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetOwnerOfAPokemon(int pokeId)
        {
       
            if (!_pokemonRepository.PokemonExists(pokeId))
            {
                return NotFound("Pokemon not found");
            }

            var owners = _ownerRepository.GetOwnerOfAPokemon(pokeId);
            var ownerDtos = _mapper.Map<List<OwnerDto>>(owners);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ownerDtos);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner([FromQuery] int countryId, [FromBody] OwnerDto ownerCreate)
        {
            if (ownerCreate == null)
                return BadRequest(ModelState);

            var owners = _ownerRepository.GetOwners()
                .Where(c => c.Name.Trim().ToUpper() == ownerCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (owners != null)
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ownerMap = _mapper.Map<Owner>(ownerCreate);

        
            ownerMap.Country = _countryRepository.GetCountry(countryId);

            if (ownerMap.Country == null)
            {
                ModelState.AddModelError("", "Country not found");
                return StatusCode(404, ModelState);
            }

            if (!_ownerRepository.CreateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{ownerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOwner(int ownerId, [FromBody] OwnerDto updatedOwner)
        {
            if (updatedOwner == null)
                return BadRequest(ModelState);

            if (ownerId != updatedOwner.Id)
                return BadRequest(ModelState);

            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var ownerMap = _mapper.Map<Owner>(updatedOwner);

            if (!_ownerRepository.UpdateOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return Ok("Updated successfully");
        }

        [HttpDelete("{ownerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOwner(int ownerId)
        {
           
            if (!_ownerRepository.OwnerExists(ownerId))
            {
                return NotFound();
            }

            var ownerToDelete = _ownerRepository.GetOwner(ownerId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            if (!_ownerRepository.DeleteOwner(ownerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
                return StatusCode(500, ModelState);
            }

            return Ok("Deleted successfully");
        }
    }
}
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
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper mapper;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this._categoryRepository = categoryRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetCategories();

            var categoryDtos = mapper.Map<List<CategoryDto>>(categories);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categoryDtos);
        }

        [HttpGet("{categoryId:int}")]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        [ProducesResponseType(404)]
        public IActionResult GetCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            var category = _categoryRepository.GetCategory(categoryId);


            var categoryDto = mapper.Map<CategoryDto>(category);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);         
            return Ok(categoryDto);
        }
        [HttpGet("pokemon/{categoryId}")]
    
        [ProducesResponseType(200, Type = typeof(IEnumerable<PokemonDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)] 
        public IActionResult GetPokemonByCategoryId(int categoryId)
        {
            var pokemons = mapper.Map<List<PokemonDto>>(
                _categoryRepository.GetPokemonByCategory(categoryId)
            );

          
            if (pokemons == null || !pokemons.Any())
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);

          
            var category = _categoryRepository.GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

          
            var categoryMap = mapper.Map<Category>(categoryCreate);

            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        [HttpPut("{categoryId}")]
        [ProducesResponseType(204)] // No Content
        [ProducesResponseType(400)] // Bad Request
        [ProducesResponseType(404)] // Not Found
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto updatedCategory)
        {
            if (updatedCategory == null)
                return BadRequest(ModelState);

          
            if (categoryId != updatedCategory.Id)
                return BadRequest(ModelState);

            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

           
            var categoryMap = mapper.Map<Category>(updatedCategory);

            
            if (!_categoryRepository.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return Ok("Updated successfully"); 
        }

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(204)] // အောင်မြင်ရင် No Content ပြန်မယ်
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int categoryId)
        {
       
            if (!_categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

          
            var categoryToDelete = _categoryRepository.GetCategory(categoryId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          
            if (!_categoryRepository.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
                return StatusCode(500, ModelState);
            }

          
            return Ok("Deleted successfully");
        }

    }
}

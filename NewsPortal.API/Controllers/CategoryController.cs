using Microsoft.AspNetCore.Mvc;
using NewsPortal.Models;

namespace NewsPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly INewsRepository repository;

        public CategoryController(INewsRepository repo)
        {
            repository = repo;
        }

        // GET: api/category
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = repository.Categories.ToList();
            return Ok(categories);
        }

        // GET: api/category/{id}
        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = repository.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        // POST: api/category
        [HttpPost]
        public IActionResult CreateCategory([FromBody] Category category)
        {
            if (repository.Categories.Any(c => c.Name == category.Name))
            {
                return BadRequest("Категорія з таким іменем вже існує");
            }

            repository.CreateCategory(category);
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        // PUT: api/category/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] Category category)
        {
            if (id != category.Id)
            {
                return BadRequest("Невірний ідентифікатор категорії");
            }

            if (repository.Categories.Any(c => c.Name == category.Name && c.Id != category.Id))
            {
                return BadRequest("Така категорія вже існує");
            }

            repository.UpdateCategory(category);
            return NoContent();
        }

        // DELETE: api/category/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = repository.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();

            repository.DeleteCategory(category);
            return NoContent();
        }
    }
}

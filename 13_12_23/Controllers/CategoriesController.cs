using _13_12_23.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _13_12_23.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRepository _repository;

        public CategoriesController(AppDbContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            IEnumerable<Category> categories = await _repository.GetAllAsync(page,take);
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest();
            Category category = await _repository.GetByIdAsync(id);

            if (category == null) return NotFound();

            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateCategoryDto createCategoryDto)
        {
            bool result = await _context.Categories.AnyAsync(c => c.Name == createCategoryDto.Name);
            if (result) return BadRequest();

            Category category = new Category { Name = createCategoryDto.Name };
            await _repository.AddAsync(category);
            await _repository.SavaChanceAsync();

            return StatusCode(StatusCodes.Status201Created, category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm]CreateCategoryDto createCategoryDto)
        {
            if (id <= 0) return BadRequest();
            Category category = await _repository.GetByIdAsync(id);

            if (category == null) return NotFound();

            bool result = await _context.Categories.AnyAsync(c => c.Name == createCategoryDto.Name && c.Id != id);
            if (result) return BadRequest();

            category.Name = createCategoryDto.Name;

            _repository.Update(category);
            await _repository.SavaChanceAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Category category = await _repository.GetByIdAsync(id);

            if (category == null) return NotFound();

            _repository.Delete(category);
            await _repository.SavaChanceAsync();

            return NoContent();
        }
    }
}

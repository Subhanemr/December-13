using _13_12_23.DAL;
using _13_12_23.Entities;
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

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            List<Category> categories = await _context.Categories.Skip((page - 1) * take).Take(take).ToListAsync();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest();
            Category category = await _context.Categories.FirstOrDefaultAsync(category => category.Id == id);

            if (category == null) return NotFound();

            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {

            bool result = await _context.Categories.AnyAsync(c => c.Name == name);
            if (result) return BadRequest();

            Category category = new Category { Name = name };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string name)
        {
            if (id <= 0) return BadRequest();
            Category category = await _context.Categories.FirstOrDefaultAsync(category => category.Id == id);

            if (category == null) return NotFound();

            bool result = await _context.Categories.AnyAsync(c => c.Name == name && c.Id != id);
            if (result) return BadRequest();

            category.Name = name;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Category category = await _context.Categories.FirstOrDefaultAsync(category => category.Id == id);

            if (category == null) return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

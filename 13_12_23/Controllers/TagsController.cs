using _13_12_23.DAL;
using _13_12_23.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _13_12_23.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TagsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            List<Tag> tags = await _context.Tags.Skip((page - 1) * take).Take(take).ToListAsync();
            return Ok(tags);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest();
            Tag tag = await _context.Tags.FirstOrDefaultAsync(category => category.Id == id);

            if (tag == null) return NotFound();

            return Ok(tag);
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {

            bool result = await _context.Categories.AnyAsync(c => c.Name == name);
            if (result) return BadRequest();

            Tag tag = new Tag { Name = name };
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, tag);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string name)
        {
            if (id <= 0) return BadRequest();
            Tag tag = await _context.Tags.FirstOrDefaultAsync(category => category.Id == id);

            if (tag == null) return NotFound();

            bool result = await _context.Tags.AnyAsync(c => c.Name == name && c.Id != id);
            if (result) return BadRequest();

            tag.Name = name;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Tag tag = await _context.Tags.FirstOrDefaultAsync(category => category.Id == id);

            if (tag == null) return NotFound();

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

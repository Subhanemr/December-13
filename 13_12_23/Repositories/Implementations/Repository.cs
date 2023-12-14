using _13_12_23.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace _13_12_23.Repositories.Implementations
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }

        public async Task<IQueryable<Category>> GetAllAsync(int page, int take, Expression<Func<Category, bool>>? expression = null, params string[] includes)
        {
            var query = _context.Categories.Skip((page - 1) * take).Take(take).AsQueryable();
            if(expression != null)
            {
                query = query.Where(expression);
            }
            if(includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            return query;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            
            return category;
        }

        public async Task SavaChanceAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async void Update(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}

using System.Linq.Expressions;

namespace _13_12_23.Repositories.Interfaces
{
    public interface IRepository
    {
        Task<IQueryable<Category>> GetAllAsync(int page, int take, Expression<Func<Category, bool>>? expression=null, params string[] includes);
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category category);
        void Update(Category category);
        void Delete(Category category);
        Task SavaChanceAsync();
    }
}

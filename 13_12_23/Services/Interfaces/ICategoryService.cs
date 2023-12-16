using System.Linq.Expressions;

namespace _13_12_23.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ICollection<GetCategoryDto>> GetAllAsync(int page, int take);
        Task<ICollection<GetCategoryDto>> GetAllByOrderAsync(int page, int take, Expression<Func<Category, object>>? orderExpression);
        Task<GetCategoryDto> GetByIdAsync(int id);
        Task CreateAsync(CreateCategoryDto categoryDto);
        Task UpdateAsync(int id, CreateCategoryDto createCategoryDto);
        Task DeleteAsync(int id);
    }
}

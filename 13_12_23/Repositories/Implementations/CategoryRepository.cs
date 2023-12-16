using _13_12_23.Repositories.Interfaces;

namespace _13_12_23.Repositories.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}

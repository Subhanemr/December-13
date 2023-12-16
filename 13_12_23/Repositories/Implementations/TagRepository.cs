using _13_12_23.Repositories.Interfaces;

namespace _13_12_23.Repositories.Implementations
{
    public class TagRepository: Repository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context): base(context) { }
    }
}

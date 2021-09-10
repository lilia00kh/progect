using DAL.Entities;
using DAL.EF;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class DetailsAboutGoodRepository : RepositoryBase<DetailsAboutGood>, IDetailsAboutGoodRepository
    {
        public DetailsAboutGoodRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
           
        }
    }
}

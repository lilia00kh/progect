using DAL.Entities;
using DAL.EF;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class BasketAndGoodRepository : RepositoryBase<BasketAndGood>, IBasketAndGoodRepository
    {
        public BasketAndGoodRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}

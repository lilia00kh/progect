using DAL.Entities;
using DAL.EF;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class BasketRepository : RepositoryBase<Basket>, IBasketRepository
    {
        public BasketRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}

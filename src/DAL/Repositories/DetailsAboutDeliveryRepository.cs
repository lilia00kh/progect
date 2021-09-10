using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class DetailsAboutDeliveryRepository : RepositoryBase<DetailsAboutDelivery>, IDetailsAboutDeliveryRepository
    {
        public DetailsAboutDeliveryRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}

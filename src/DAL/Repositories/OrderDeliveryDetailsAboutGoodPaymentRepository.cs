using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class OrderDeliveryDetailsAboutGoodPaymentRepository : RepositoryBase<OrderDeliveryDetailsAboutGoodPayment>, IOrderDeliveryDetailsAboutGoodPaymentRepository
    {
        public OrderDeliveryDetailsAboutGoodPaymentRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}

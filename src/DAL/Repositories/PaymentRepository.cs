using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;


namespace DAL.Repositories
{
    public class PaymentRepository : RepositoryBase<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}

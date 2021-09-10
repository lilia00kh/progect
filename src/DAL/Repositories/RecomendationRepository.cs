using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class RecomendationRepository : RepositoryBase<Recomendation>, IRecomendationRepository
    {
        public RecomendationRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}

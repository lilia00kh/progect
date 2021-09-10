using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ImageRepository : RepositoryBase<Image>, IImageRepository
    {
        public ImageRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}

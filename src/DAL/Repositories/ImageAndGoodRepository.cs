using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ImageAndGoodRepository : RepositoryBase<ImageAndGood>, IImageAndGoodRepository
    {
        public ImageAndGoodRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
            _ = repositoryContext.ImageAndGood.Include(x => x.Image);
        }
    }
}

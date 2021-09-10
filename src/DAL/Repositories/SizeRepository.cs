using DAL.Entities;
using DAL.EF;
using DAL.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SizeRepository : RepositoryBase<Size>, ISizeRepository
    {
        public SizeRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task CreateSizeAsync(Size size)
        {
            await Create(size);
            
        }

        public async Task<IEnumerable<Size>> GetAllSizesAsync()
        {
            var sizes = await FindAll(trackChanges: false);
            return sizes;
        }
    }
}

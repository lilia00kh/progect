using DAL.Entities;
using DAL.EF;
using DAL.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class TreeSizeAndPriceRepository : RepositoryBase<TreeSizeAndPrice>, ITreeSizeAndPriceRepository
    {
        public TreeSizeAndPriceRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task CreateTreeSizeAndPriceAsync(TreeSizeAndPrice treeSizeAndPrice)
        {
            await Create(treeSizeAndPrice);
        }

        public async Task<IQueryable<TreeSizeAndPrice>> GetAllTreeSizesAndPricesAsync(Guid id)
        {
            var res = await FindByCondition(x => x.TreeId == id, false);
            return res.Include(x=>x.Size).Include(x=>x.Tree);
        }

        public async Task<TreeSizeAndPrice> FindByIdAsync(Guid id)
        {
            var res = await FindByCondition(x => x.Id == id, trackChanges: false);
            return res.FirstOrDefault();
        }

        public async Task<IEnumerable<TreeSizeAndPrice>> FindByTreeIdAsync(Guid id)
        {
            var res = await FindByCondition(x => x.TreeId == id, trackChanges: false);
            return res;
        }

        public async Task<IEnumerable<TreeSizeAndPrice>> FindByTreeIdWithDetailsAsync(Guid id)
        {
            var res = await FindByCondition(x => x.TreeId == id, trackChanges: false);
            return res.Include(x => x.Size).Include(x => x.Tree);
        }

        public async Task DeleteTreeSizeAndPriceAsync(TreeSizeAndPrice treeSizeAndPrice)
        {
            await Delete(treeSizeAndPrice);
        }

        public async Task UpdateTreeSizeAndPriceAsync(TreeSizeAndPrice treeSizeAndPrice)
        {
            await Update(treeSizeAndPrice);
        }
    }
}

using DAL.Entities;
using DAL.EF;
using DAL.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TreeRepository : RepositoryBase<Tree>, ITreeRepository
    {
        public TreeRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }
        public async Task CreateTreeAsync(Tree tree)
        {
            await Create(tree);
        }

        public async Task<IEnumerable<Tree>> GetAllTreesAsync()
        {
            var trees = await FindAll(trackChanges: false);
            return trees;
        }

        public async Task DeleteTreesAsync(Tree tree)
        {
            await Delete(tree);
        }

        public async Task UpdateTreeAsync(Tree tree)
        {
            await Update(tree);
        }

        public async Task<Tree> FindByIdAsync(Guid id)
        {
            var trees = await FindByCondition(x => x.Id == id, trackChanges: false);
            return trees.FirstOrDefault();
        }
    }
}

using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ITreeRepository
    {
        Task CreateTreeAsync(Tree tree);
        Task<IEnumerable<Tree>> GetAllTreesAsync();
        Task DeleteTreesAsync(Tree tree);
        Task<Tree> FindByIdAsync(Guid id);
        Task UpdateTreeAsync(Tree tree);
    }
}

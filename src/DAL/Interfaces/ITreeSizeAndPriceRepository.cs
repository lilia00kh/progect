using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ITreeSizeAndPriceRepository
    {
        Task CreateTreeSizeAndPriceAsync(TreeSizeAndPrice treeSizeAndPrice);

        Task<IQueryable<TreeSizeAndPrice>> GetAllTreeSizesAndPricesAsync(Guid id);
        Task<TreeSizeAndPrice> FindByIdAsync(Guid id);
        Task<IEnumerable<TreeSizeAndPrice>> FindByTreeIdAsync(Guid id);
        Task<IEnumerable<TreeSizeAndPrice>> FindByTreeIdWithDetailsAsync(Guid id);
        Task DeleteTreeSizeAndPriceAsync(TreeSizeAndPrice treeSizeAndPrice);
        Task UpdateTreeSizeAndPriceAsync(TreeSizeAndPrice treeSizeAndPrice);
    }
}

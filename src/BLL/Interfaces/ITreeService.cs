using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.EntitiesDTO;

namespace BLL.Interfaces
{
    public interface ITreeService
    {
        Task<Guid> CreateSizeAsync(SizeDto sizeDto); 
        Task<Guid> CreateTreeAsync(TreeDto treeDto);
        Task CreateTreeSizeAndPriceAsync(TreeSizeAndPriceDto treeSizeAndPriceDto);
        Task<IEnumerable<TreeDto>> GetAllTreesAsync();
        Task DeleteTreeAsync(Guid id);
        Task<IEnumerable<TreeSizeAndPriceDto>> GetAllTreeSizesAndPricesAsync(Guid id);
        Task<TreeDto> GetTreeByIdAsync(Guid id);
        Task<IEnumerable<TreeSizeAndPriceDto>> GetTreePricesAndSizesByTreeIdAsync(Guid id);
        Task UpdateTreeSizeAndPriceAsync(TreeSizeAndPriceDto treeSizeAndPriceDto);
        Task UpdateTreeAsync(TreeDto treeDto);
        Task DeleteTreePriceAndSizeAsync(Guid id);
        Task<Guid> CreateImageAsync(ImageDto imageDto);
        Task CreateImageAndGoodAsync(ImageAndGoodDto imageAndGoodDto);
        Task<List<TreeDto>> SearchByName(string name);
        Task<List<ImageDto>> GetImagesByToyIdAsync(Guid id);
        Task<bool> ImageExistInDB(string imageName);
        Task<IEnumerable<TreeDto>> GetAllTreesByFilterAsync(FilterDto filterDto);
    }
}

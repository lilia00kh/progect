using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.EntitiesDTO;

namespace BLL.Interfaces
{
    public interface IToyService
    {
        Task<IEnumerable<ToyDto>> GetAllToysAsync();
        Task<Guid> CreateToyAsync(ToyDto toyDto);
        Task DeleteToyAsync(Guid id);
        Task UpdateToyAsync(ToyDto toyDto);
        Task<ToyDto> GetToyByIdAsync(Guid id);
        Task<List<ImageDto>> GetImagesByToyIdAsync(Guid id);
        Task<Guid> CreateImageAsync(ImageDto imageDto);
        Task CreateImageAndGoodAsync(ImageAndGoodDto imageAndGoodDto);
        Task<bool> ImageExistInDB(string imageName);
        Task<List<ToyDto>> SearchByName(string name);
    }
}

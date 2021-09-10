using BLL.EntitiesDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISearchAndRecomendationResponseService
    {
        Task<SearchAndRecomendationResponseDto> SearchByName(string name);
        Task<List<ImageDto>> GetImagesByIdAsync(Guid id);
        Task CreateRecomendationAsync(RecomendationDto recomendationDto);
        Task<SearchAndRecomendationResponseDto> GetAllRecomendations();
        Task DeleteRecomendationByGoodId(Guid goodId);
    }
}

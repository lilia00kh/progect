using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.EntitiesDTO;

namespace BLL.Interfaces
{
    public interface IBasketService
    {
        Task<Guid> CreateDetailsAboutGoodAsync(DetailsAboutGoodDto detailsAboutGoodDto);
        Task AddTreeToBasketAsync(string userName, Guid treeId, DetailsAboutGoodDto detailsAboutGoodDto);
        Task DeleteGoodFromBasketAsync(Guid detailsId);
        Task AddToyToBasketAsync(string userName, Guid toyId, DetailsAboutGoodDto detailsAboutGoodDto);
        Task<BasketDto> GetBasket(string userName);
        Task<IEnumerable<DetailsAboutGoodDto>> GetAllGoodsFromBasket(string userName);
        Task<List<ImageDto>> GetImagesByGoodId(Guid goodId);
    }
}

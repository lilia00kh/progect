using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EntitiesDTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class BasketService : IBasketService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public BasketService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _database = unitOfWork;
        }

        public async Task<Guid> CreateDetailsAboutGoodAsync(DetailsAboutGoodDto detailsAboutGoodDto)
        {
            var details = new DetailsAboutGood()
            {
                TypeOfGood = detailsAboutGoodDto.TypeOfGood,
                Count = detailsAboutGoodDto.Count,
                Size = detailsAboutGoodDto.Size,
                Price = detailsAboutGoodDto.Price,
                Color = detailsAboutGoodDto.Color
            };
            await _database.DetailsAboutGood.Create(details);
            _database.Save();
            return details.Id;
        }

        public async Task<DetailsAboutGoodDto> GetDetailsAboutGoodAsync(Guid detailsAboutGoodId)
        {
            var detailsAboutGoodIQueryable = await _database.DetailsAboutGood.FindByCondition(x => x.Id == detailsAboutGoodId, trackChanges: false);
            var detailsAboutGood = detailsAboutGoodIQueryable.FirstOrDefault();
            return new DetailsAboutGoodDto() { 
                Id = detailsAboutGood.Id,
                TypeOfGood = detailsAboutGood.TypeOfGood,
                Size = detailsAboutGood.Size,
                Price = detailsAboutGood.Price,
                Count = detailsAboutGood.Count,
                Color = detailsAboutGood.Color
            };
        }

        public async Task<List<ImageDto>> GetImagesByGoodId(Guid goodId)
        {
            var imageDtos = new List<ImageDto>();
            var imageAndGood = (await _database.ImageAndGood.FindByCondition(x => x.GoodId == goodId, trackChanges: false)).ToList();
            foreach (var i in imageAndGood)
            {
                var image = (await _database.Image.FindByCondition(x => x.Id == i.ImageId, trackChanges: false)).FirstOrDefault();
                imageDtos.Add(_mapper.Map<Image, ImageDto>(image));
            }
            return imageDtos;
        }

        public async Task AddTreeToBasketAsync(string userName, Guid treeId, DetailsAboutGoodDto detailsAboutGoodDto)
        {
            var basket = await GetBasket(userName);
            var basketAndGoodIQueryable = await _database.BasketAndGood.FindByCondition(x => x.BasketId == basket.Id && x.GoodId == treeId, trackChanges: false);
            var basketAndGood = basketAndGoodIQueryable.ToList();
            foreach(var item in basketAndGood)
            {
                var detailsAboutGoodIQueryable = await _database.DetailsAboutGood.FindByCondition(x => x.Id == item.DetailsAboutGoodId, trackChanges: false);
                var detailsAboutGood = detailsAboutGoodIQueryable.FirstOrDefault(x => x.Size == detailsAboutGoodDto.Size);
                if (detailsAboutGood!=null)
                {
                    detailsAboutGood.Count += detailsAboutGoodDto.Count;
                    detailsAboutGood.Price += detailsAboutGoodDto.Price;
                    await _database.DetailsAboutGood.Update(detailsAboutGood);
                    _database.Save();
                    return;
                }
                
            }
            detailsAboutGoodDto.Id = await CreateDetailsAboutGoodAsync(detailsAboutGoodDto);
            var newBasketAndGood = new BasketAndGood() { GoodId = treeId, BasketId = basket.Id, DetailsAboutGoodId = detailsAboutGoodDto.Id };
            await _database.BasketAndGood.Create(newBasketAndGood);
            _database.Save();
        }

        public async Task AddToyToBasketAsync(string userName, Guid toyId, DetailsAboutGoodDto detailsAboutGoodDto)
        {
            var basket = await GetBasket(userName);
            var basketAndGoodIQueryable = await _database.BasketAndGood.FindByCondition(x => x.BasketId == basket.Id && x.GoodId == toyId, trackChanges: false);
            var basketAndGood = basketAndGoodIQueryable.ToList();
            foreach (var item in basketAndGood)
            {
                var detailsAboutGoodIQueryable = await _database.DetailsAboutGood.FindByCondition(x => x.Id == item.DetailsAboutGoodId, trackChanges: false);
                var detailsAboutGood = detailsAboutGoodIQueryable.FirstOrDefault();
                if (detailsAboutGood != null)
                {
                    detailsAboutGood.Count += detailsAboutGoodDto.Count;
                    detailsAboutGood.Price += detailsAboutGoodDto.Price;
                    await _database.DetailsAboutGood.Update(detailsAboutGood);
                    _database.Save();
                    return;
                }

            }
            detailsAboutGoodDto.Id = await CreateDetailsAboutGoodAsync(detailsAboutGoodDto);
            var newBasketAndGood = new BasketAndGood() { GoodId = toyId, BasketId = basket.Id, DetailsAboutGoodId = detailsAboutGoodDto.Id };
            await _database.BasketAndGood.Create(newBasketAndGood);
            _database.Save();
        }
        public async Task<BasketDto> GetBasket(string userName)
        {
            var basketIQueryable = await _database.Basket.FindByCondition(x => x.UserName == userName, trackChanges: false);
            return _mapper.Map< Basket, BasketDto> (basketIQueryable.FirstOrDefault());
        }

        public async Task<IEnumerable<BasketAndGoodDto>> GetBasketAndGood(Guid basketId)
        {
            var basketAndGood = await _database.BasketAndGood.FindByCondition(x => x.BasketId == basketId, trackChanges: false);
            return _mapper.Map<IEnumerable<BasketAndGood>, IEnumerable<BasketAndGoodDto>>(basketAndGood);
        }

        public async Task<IEnumerable<DetailsAboutGoodDto>> GetAllGoodsFromBasket(string userName)
        {
            var basket = await GetBasket(userName);
            var basketAndGood = await GetBasketAndGood(basket.Id);
            List<DetailsAboutGoodDto> detailsAboutGoodDtos = new List<DetailsAboutGoodDto>();
            foreach (var item in basketAndGood)
            {
                var detailsAboutGoodIQueryable = await _database.DetailsAboutGood.FindByCondition(x => x.Id == item.DetailsAboutGoodId, trackChanges: false);
                var detailsAboutGood = _mapper.Map<DetailsAboutGood, DetailsAboutGoodDto>(detailsAboutGoodIQueryable.FirstOrDefault());
                if(detailsAboutGood.TypeOfGood=="tree")
                {
                    var tree = await _database.Tree.FindByIdAsync(item.GoodId);
                    detailsAboutGood.Name = tree.Name;
                    detailsAboutGood.GoodId = tree.Id;
                }
                else {
                    var toy = await _database.Toy.FindByIdAsync(item.GoodId);
                    detailsAboutGood.Name = toy.Name;
                    detailsAboutGood.GoodId = toy.Id;
                }
               
                detailsAboutGoodDtos.Add(detailsAboutGood);
            }

            return detailsAboutGoodDtos;
        }

        public async Task DeleteGoodFromBasketAsync(Guid detailsId)
        {
            var basketAndGoodIQueryable = await _database.BasketAndGood.FindByCondition(x=>x.DetailsAboutGoodId == detailsId, trackChanges: false);
            var basketAndGood = basketAndGoodIQueryable.FirstOrDefault();
            var detailsAboutGoodIQueryable = await _database.DetailsAboutGood.FindByCondition(x => x.Id == detailsId, trackChanges: false);
            var detailsAboutGood = detailsAboutGoodIQueryable.FirstOrDefault();
            await _database.DetailsAboutGood.Delete(detailsAboutGood);
            await _database.BasketAndGood.Delete(basketAndGood);
            _database.Save();
        }
    }
}

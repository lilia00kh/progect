using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EntitiesDTO;
using BLL.Infrastracture;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class SearchAndRecomendationResponseService : ISearchAndRecomendationResponseService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;
        public SearchAndRecomendationResponseService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _database = unitOfWork;
        }
        public async Task<SearchAndRecomendationResponseDto> SearchByName(string name)
        {
            var trees = await GetAllTreesAsync();
            var treesDtoForSerch = trees.Where(x => x.Name.ToLower().Contains(name)).ToList();
            var toys = await GetAllToysAsync();
            var toysDtoForSerch = toys.Where(x => x.Name.ToLower().Contains(name)).ToList();
            return new SearchAndRecomendationResponseDto()
            {
                Toys = toysDtoForSerch,
                Trees = treesDtoForSerch
            };
        }

        public async Task<List<ImageDto>> GetImagesByIdAsync(Guid id)
        {
            var imageDtoList = new List<ImageDto>();
            var imageAndGood = (await _database.ImageAndGood.FindByCondition(x => x.GoodId == id, trackChanges: false)).ToList();
            foreach (var i in imageAndGood)
            {
                var image = (await _database.Image.FindByCondition(x => x.Id == i.ImageId, trackChanges: false)).FirstOrDefault();
                imageDtoList.Add(_mapper.Map<Image, ImageDto>(image));
            }
            return imageDtoList;
        }
        public async Task<IEnumerable<TreeDto>> GetAllTreesAsync()
        {
            var trees = await _database.Tree.GetAllTreesAsync();
            var treeDtos = _mapper.Map<IEnumerable<Tree>, IEnumerable<TreeDto>>(trees);
            foreach (var tree in treeDtos)
            {
                var imagesDto = await GetImagesByIdAsync(tree.Id);
                tree.ImageDtos = imagesDto.ToList();
                var priceAndSizeDtos = await GetTreePricesAndSizesByTreeIdAsync(tree.Id);
                tree.PriceAndSizeDtos = priceAndSizeDtos.ToList();
            }
            return treeDtos;
        }

        public async Task<IEnumerable<TreeSizeAndPriceDto>> GetTreePricesAndSizesByTreeIdAsync(Guid id)
        {
            var treeSizeAndPrice = await _database.TreeSizeAndPrice.FindByTreeIdWithDetailsAsync(id);
            if (treeSizeAndPrice == null)
                throw new CustomException("Ялинка немає розміру і ціни", "");
            return _mapper.Map<IEnumerable<TreeSizeAndPrice>, IEnumerable<TreeSizeAndPriceDto>>(treeSizeAndPrice);
        }

        public async Task<IEnumerable<ToyDto>> GetAllToysAsync()
        {
            var toys = await _database.Toy.GetAllToysAsync();
            if (toys.Count() == 0)
                throw new CustomException("Список прикрас порожній", "");
            var toyDtos = _mapper.Map<IEnumerable<Toy>, IEnumerable<ToyDto>>(toys);
            foreach (var toy in toyDtos)
            {
                var imagesDto = await GetImagesByIdAsync(toy.Id);
                toy.ImageDtos = imagesDto.ToList();
            }
            return toyDtos;
        }
        public async Task<ToyDto> GetToyByIdAsync(Guid id)
        {
            var toy = await _database.Toy.FindByIdAsync(id);
            if (toy == null)
                throw new CustomException("Toy does not exist", "");
            var toyDto = _mapper.Map<Toy, ToyDto>(toy);
            var imagesDto = await GetImagesByIdAsync(toy.Id);
            toyDto.ImageDtos = imagesDto.ToList();
            return toyDto;

        }

        public async Task<TreeDto> GetTreeByIdAsync(Guid id)
        {
            var tree = await _database.Tree.FindByIdAsync(id);
            if (tree == null)
                throw new CustomException("Такої ялинки не існує", "");
            var treeDto = _mapper.Map<Tree, TreeDto>(tree);
            var priceAndSizeDtos = await GetTreePricesAndSizesByTreeIdAsync(id);
            var imagesDto = await GetImagesByIdAsync(id);
            treeDto.ImageDtos = imagesDto.ToList();
            treeDto.PriceAndSizeDtos = priceAndSizeDtos.ToList();
            return treeDto;
        }
        public async Task CreateRecomendationAsync(RecomendationDto recomendationDto)
        {
            var recomandations = await GetAllRecomendations();
            if (recomendationDto.GoodType=="прикраса")
            {
                if (recomandations.Toys.Any(x => x.Id == recomendationDto.GoodId))
                    throw new CustomException("Прикрасу вже додано в рекомендації", "");
            }
            else
            {
                if (recomandations.Trees.Any(x => x.Id == recomendationDto.GoodId))
                    throw new CustomException("Ялинку вже додано в рекомендації", "");
            }
            await _database.Recomendation.Create(new Recomendation() {
                GoodId = recomendationDto.GoodId,
                GoodType = recomendationDto.GoodType
            });
            _database.Save();
        }

        public async Task<SearchAndRecomendationResponseDto> GetAllRecomendations()
        {
            var allRecomendations = await _database.Recomendation.FindAll(trackChanges:false);
            var treeDtos = new List<TreeDto>();
            var toyDtos = new List<ToyDto>();
            foreach (var recomendation in allRecomendations)
            {
                if(recomendation.GoodType=="ялинка")
                {
                    var tree = await GetTreeByIdAsync(recomendation.GoodId);
                    treeDtos.Add(tree);
                }    
                else
                {
                    var toy = await GetToyByIdAsync(recomendation.GoodId);
                    toyDtos.Add(toy);
                }
            }
            return new SearchAndRecomendationResponseDto()
            {
                Toys = toyDtos,
                Trees = treeDtos
            };
        }

        public async Task DeleteRecomendationByGoodId(Guid goodId)
        {
            var recomendation = await _database.Recomendation.FindByCondition(x => x.GoodId == goodId, trackChanges: false);
            if (recomendation.Count() == 0)
                throw new CustomException("Такої рекомендації не існує", "");
            await _database.Recomendation.Delete(recomendation.FirstOrDefault());
            _database.Save();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EntitiesDTO;
using BLL.Infrastracture;
using BLL.Interfaces;
using BLL.JwtFeatures;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class TreeService : ITreeService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;
        
        public TreeService( IMapper mapper, JwtHandler jwtHandler, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;            
            _database = unitOfWork;            
        }

        public async Task<Guid> CreateSizeAsync(SizeDto sizeDto)
        {
            var allSizes = await _database.Size.GetAllSizesAsync();
            var allSizesList = allSizes.ToList();
            if (!allSizesList.Any())
            {
                return await CreateSize(sizeDto.NameOfSize);
            }
            var size = allSizesList.FirstOrDefault(x => x.NameOfSize == sizeDto.NameOfSize);
            if (size != null) return size.Id;
            return await CreateSize(sizeDto.NameOfSize);
        }

        private async Task<Guid> CreateSize(double nameOfSizeDto)
        {
            var size = new Size() { NameOfSize = nameOfSizeDto };
            await _database.Size.CreateSizeAsync(size);
            _database.Save();
            return size.Id;
        }

        public async Task<Guid> CreateTreeAsync(TreeDto treeDto)
        {
            var tree =new Tree()
            {
                //Id = new Guid(),
                Name = treeDto.Name,
                Description = treeDto.Description,
                Color = treeDto.Color,
                TreeType = treeDto.TreeType
            };
            await _database.Tree.CreateTreeAsync(tree);
            _database.Save();
            return tree.Id;

        }

        public async Task UpdateTreeAsync(TreeDto treeDto)
        {
            var tree = new Tree()
            {
                Id = treeDto.Id,
                Name = treeDto.Name,
                Description = treeDto.Description,
                Color = treeDto.Color,
                TreeType = treeDto.TreeType
            };
            await _database.Tree.UpdateTreeAsync(tree);
            _database.Save();
        }

        public async Task CreateTreeSizeAndPriceAsync(TreeSizeAndPriceDto treeSizeAndPriceDto)
        {
            await _database.TreeSizeAndPrice.CreateTreeSizeAndPriceAsync(new TreeSizeAndPrice()
            {
                Price = treeSizeAndPriceDto.Price,
                SizeId = treeSizeAndPriceDto.SizeDtoId,
                TreeId = treeSizeAndPriceDto.TreeDtoId
            });
            _database.Save();
        }

        public async Task UpdateTreeSizeAndPriceAsync(TreeSizeAndPriceDto treeSizeAndPriceDto)
        {
            var treeSizeAndPrice = _mapper.Map<TreeSizeAndPriceDto, TreeSizeAndPrice>(treeSizeAndPriceDto);
            await _database.TreeSizeAndPrice.UpdateTreeSizeAndPriceAsync(new TreeSizeAndPrice()
            {
                Id = treeSizeAndPrice.Id,
                Price = treeSizeAndPrice.Price,
                Tree = treeSizeAndPrice.Tree,
                Size = treeSizeAndPrice.Size,
                SizeId = treeSizeAndPrice.SizeId,
                TreeId = treeSizeAndPrice.TreeId
            });
            _database.Save();
        }

        public async Task<IEnumerable<TreeDto>> GetAllTreesAsync()
        {
            var trees = await _database.Tree.GetAllTreesAsync();
            var treeDtos = _mapper.Map<IEnumerable<Tree>, IEnumerable<TreeDto>>(trees);
            foreach(var tree in treeDtos)
            {
                var imagesDto = await GetImagesByToyIdAsync(tree.Id);
                tree.ImageDtos = imagesDto.ToList();
                var priceAndSizeDtos = await GetTreePricesAndSizesByTreeIdAsync(tree.Id);
                tree.PriceAndSizeDtos = priceAndSizeDtos.ToList();
            }
            return treeDtos;
        }

        public async Task<TreeDto> GetTreeByIdAsync(Guid id)
        {
            var tree = await _database.Tree.FindByIdAsync(id);
            if (tree == null)
                throw new CustomException("Такої ялинки не існує", "");
            var treeDto = _mapper.Map<Tree, TreeDto>(tree);
            var priceAndSizeDtos = await GetTreePricesAndSizesByTreeIdAsync(id);
            var imagesDto = await GetImagesByToyIdAsync(id);
            treeDto.ImageDtos = imagesDto.ToList();
            treeDto.PriceAndSizeDtos = priceAndSizeDtos.ToList();
            return treeDto;
        }

        public async Task<IEnumerable<TreeSizeAndPriceDto>> GetTreePricesAndSizesByTreeIdAsync(Guid id)
        {
            var treeSizeAndPrice = await _database.TreeSizeAndPrice.FindByTreeIdWithDetailsAsync(id);
            if (treeSizeAndPrice == null)
                throw new CustomException("Tree does not have size and price", "");
            return _mapper.Map<IEnumerable<TreeSizeAndPrice>, IEnumerable<TreeSizeAndPriceDto>>(treeSizeAndPrice);
        }
        public async Task<List<TreeDto>> SearchByName(string name)
        {
            var allTrees = await GetAllTreesAsync();
            var treesWithSubstringName = allTrees.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            return treesWithSubstringName.ToList();
        }

        public async Task<IEnumerable<TreeSizeAndPriceDto>> GetAllTreeSizesAndPricesAsync(Guid id)
        {
            var treeSizesAndPrices = await _database.TreeSizeAndPrice.GetAllTreeSizesAndPricesAsync(id);
            var treeSizesAndPricesList = treeSizesAndPrices.ToList();

            return _mapper.Map<IEnumerable<TreeSizeAndPrice>, IEnumerable<TreeSizeAndPriceDto>>(treeSizesAndPricesList); 
            
        }

        public async Task DeleteTreeAsync(Guid id)
        {
            var tree = await _database.Tree.FindByIdAsync(id);
            if (tree == null)
                throw new CustomException($"Tree with id {id} does not exist", "");
            await _database.Tree.DeleteTreesAsync(tree);
            var treesSizeAndPrice = await _database.TreeSizeAndPrice.FindByTreeIdAsync(id);
            foreach (var treeSizeAndPrice in treesSizeAndPrice)
            {
                await _database.TreeSizeAndPrice.DeleteTreeSizeAndPriceAsync(treeSizeAndPrice);
            }
            var basketGoods = await _database.BasketAndGood.FindByCondition(x => x.GoodId == id, trackChanges: false);
            foreach (var basketGood in basketGoods)
            {
                await _database.BasketAndGood.Delete(basketGood);
            }
            var imageAndGood = (await _database.ImageAndGood.FindByCondition(x => x.GoodId == id, trackChanges: false)).ToList();
            foreach(var i in imageAndGood) {
                var image = (await _database.Image.FindByCondition(x => x.Id == i.ImageId, trackChanges: false)).FirstOrDefault();
                var imagePath = image.ImagePath;
                DeleteImage(imagePath);
                await _database.Image.Delete(image);
                await _database.ImageAndGood.Delete(i);
            }
            var recomandationd = (await _database.Recomendation.FindByCondition(x => x.GoodId == id, trackChanges: false)).ToList();
            foreach (var r in recomandationd)
            {
                await _database.Recomendation.Delete(r);
            }
            _database.Save();
        }

        private void DeleteImage(string imagePath)
        {
            string Path = imagePath;
            FileInfo file = new FileInfo(Path);
            if (file.Exists)
            {
                file.Delete();
            }
        }

        public async Task DeleteTreePriceAndSizeAsync(Guid id)
        {
            if(id.ToString() == "00000000-0000-0000-0000-000000000000")
                return;
            var treePriceAndSize = await _database.TreeSizeAndPrice.FindByIdAsync(id);
            if (treePriceAndSize == null)
                throw new CustomException($"Tree Price And Size with {id} does not exist", "");
            await _database.TreeSizeAndPrice.DeleteTreeSizeAndPriceAsync(treePriceAndSize);
            _database.Save();
        }

        public async Task<bool> ImageExistInDB(string imageName)
        {
            var imageThatExistInDB = (await _database.Image.FindByCondition(x => x.ImageName == imageName, trackChanges: false)).FirstOrDefault();
            if (imageThatExistInDB != null)
                return true;
            return false;
        }

        public async Task<Guid> CreateImageAsync(ImageDto imageDto)
        {

            var image = _mapper.Map<ImageDto, Image>(imageDto);
            await _database.Image.Create(image);
            _database.Save();
            return image.Id;
        }
        public async Task CreateImageAndGoodAsync(ImageAndGoodDto imageAndGoodDto)
        {
            await _database.ImageAndGood.Create(_mapper.Map<ImageAndGoodDto, ImageAndGood>(imageAndGoodDto));
            _database.Save();
        }

        public async Task<List<ImageDto>> GetImagesByToyIdAsync(Guid id)
        {
            var imageDtoList = new List<ImageDto>();
            var imageAndGood = (await _database.ImageAndGood.FindByCondition(x => x.GoodId == id, trackChanges: false)).ToList();
            foreach (var i in imageAndGood)
            {
                var image = await _database.Image.FindByCondition(x => x.Id == i.ImageId, trackChanges: false);
                imageDtoList.Add(_mapper.Map<Image, ImageDto>(image.FirstOrDefault()));
            }
            return imageDtoList;
        }

        public async Task<IEnumerable<TreeDto>> GetAllTreesByFilterAsync(FilterDto filterDto)
        {
            var trees = (await GetAllTreesAsync()).ToList();
            var treesFilteredByAllColor = new List<TreeDto>();
            if (!filterDto.TreeColors.Any())
                treesFilteredByAllColor = trees;
            foreach (var filterColor in filterDto.TreeColors)
            {
                var treesFilteredByOneColor = trees.Where(x => x.Color == filterColor);
                foreach (var tree in treesFilteredByOneColor)
                    treesFilteredByAllColor.Add(tree);
            }
            var treeFilteredByAllSizesAndPrices = new List<TreeDto>();
            if (!filterDto.TreeSizes.Any())
                treeFilteredByAllSizesAndPrices = treesFilteredByAllColor;
            foreach (var tree in treesFilteredByAllColor)
            {
                var treePricesAndSizes = await GetTreePricesAndSizesByTreeIdAsync(tree.Id);
                
                foreach (var filterSize in filterDto.TreeSizes)
                {
                    if (treePricesAndSizes.Any(x => x.Size.NameOfSize == filterSize && x.Price>=filterDto.MinPrice && x.Price <= filterDto.MaxPrice)) {
                        treeFilteredByAllSizesAndPrices.Add(tree); 
                        break; 
                    }
                       
                }
            }
            return treeFilteredByAllSizesAndPrices;


        }
    }
}

using BLL.EntitiesDTO;
using PL.Models;
using System.Collections.Generic;
using System.Linq;

namespace PL.Mapping
{
    public class Mapper
    {
        public static TreeModel MapTreeModel(TreeDto treeDto)
        {
            var priceAndSizeModels = MapTreePricesAndSizeList(treeDto.PriceAndSizeDtos);
            var images = MapImageList(treeDto.ImageDtos);
            return new TreeModel()
            {
                Id = treeDto.Id,
                Name = treeDto.Name,
                Description = treeDto.Description,
                PriceAndSizeModels = priceAndSizeModels,
                Color = treeDto.Color,
                ImageModels = images
            };
        }
        public static List<TreeModel> MapTreeModelList(IEnumerable<TreeDto> treeDtos, string treeType)
        {
            var treesModel = new List<TreeModel>();
            IEnumerable<TreeDto> treeDtosWithType;
            if (treeType != "all")
                treeDtosWithType = treeDtos.Where(x => x.TreeType == treeType);
            else
                treeDtosWithType = treeDtos;
            foreach (var tree in treeDtosWithType)
            {
                treesModel.Add(MapTreeModel(tree));
            }
            return treesModel;
        }

        public static List<ImageModel> MapImageList(IEnumerable<ImageDto> imagesDto)
        {
            var imagesModel = new List<ImageModel>();
            foreach (var image in imagesDto)
            {
                imagesModel.Add(new ImageModel()
                {
                    ImageName = image.ImageName,
                    ImagePath = image.ImagePath,

                });
            }
            return imagesModel;
        }

        public static List<PriceAndSizeModel> MapTreePricesAndSizeList(IEnumerable<TreeSizeAndPriceDto> priceAndSizeDtos)
        {
            List<PriceAndSizeModel> priceAndSizeModels = new List<PriceAndSizeModel>();
            foreach (var pricesAndSize in priceAndSizeDtos)
            {
                priceAndSizeModels.Add(new PriceAndSizeModel()
                {
                    Id = pricesAndSize.Id,
                    Price = pricesAndSize.Price,
                    Size = pricesAndSize.Size.NameOfSize
                });
            }
            return priceAndSizeModels;
        }

        public static SearchAndRecomendationResponseModel MapSearchAndRecomendationResponseModel(SearchAndRecomendationResponseDto searchAndRecomendationResponseDto)
        {
            var toysModel = MapToyModelList(searchAndRecomendationResponseDto.Toys);
            var treesModel = MapTreeModelList(searchAndRecomendationResponseDto.Trees,"all");
            return new SearchAndRecomendationResponseModel()
            {
                Trees = treesModel,
                Toys = toysModel
            };
        }

        public static ToyModel MapToyModel(ToyDto toyDto)
        {
            var images = MapImageList(toyDto.ImageDtos);
            return new ToyModel()
            {
                Id = toyDto.Id,
                Name = toyDto.Name,
                Description = toyDto.Description,
                Price = toyDto.Price,
                ImageModels = images
            };
        }
        public static List<ToyModel> MapToyModelList(IEnumerable<ToyDto> toysDto)
        {
            var toysModel = new List<ToyModel>();
            foreach (var toy in toysDto)
            {
                toysModel.Add(MapToyModel(toy));
            }
            return toysModel;
        }

    }
}

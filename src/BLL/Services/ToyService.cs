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
    public class ToyService : IToyService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;
        private readonly JwtHandler _jwtHandler;

        public ToyService(IMapper mapper, JwtHandler jwtHandler, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _jwtHandler = jwtHandler;
            _database = unitOfWork;

        }

        public async Task<Guid> CreateToyAsync(ToyDto toyDto)
        {
            var toy = _mapper.Map<ToyDto, Toy>(toyDto);
            await _database.Toy.CreateToyAsync(toy);
            _database.Save();
            return toy.Id;
        }

        public async Task DeleteToyAsync(Guid id)
        {
            var toy = await _database.Toy.FindByIdAsync(id);
            if (toy == null)
                throw new CustomException($"Toy with id {id} does not exist", "");
            await _database.Toy.DeleteToyAsync(toy);
            var basketGoods = await _database.BasketAndGood.FindByCondition(x => x.GoodId == id, trackChanges: false);
            foreach (var basketGood in basketGoods)
            {
                await _database.BasketAndGood.Delete(basketGood);
            }
            var imageAndGood = (await _database.ImageAndGood.FindByCondition(x => x.GoodId == id, trackChanges: false)).ToList();
            foreach (var i in imageAndGood)
            {
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

        public async Task<IEnumerable<ToyDto>> GetAllToysAsync()
        {
            var toys = await _database.Toy.GetAllToysAsync();
            if (toys.Any())
                throw new CustomException("Список прикрас порожній", "");
            var toyDtos = _mapper.Map<IEnumerable<Toy>, IEnumerable<ToyDto>>(toys);
            foreach (var toy in toyDtos)
            {
                var imagesDto = await GetImagesByToyIdAsync(toy.Id);
                toy.ImageDtos = imagesDto.ToList();
            }
            return toyDtos;
        }

        public async Task<List<ToyDto>> SearchByName(string name)
        {
            var allToys = await GetAllToysAsync();
            var toysWithSubstringName = allToys.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            return toysWithSubstringName.ToList();
        }

        public async Task<ToyDto> GetToyByIdAsync(Guid id)
        {
            var toy = await _database.Toy.FindByIdAsync(id);
            if (toy == null)
                throw new CustomException("Toy does not exist", "");
            var toyDto = _mapper.Map<Toy, ToyDto>(toy);
            var imagesDto = await GetImagesByToyIdAsync(toy.Id);
            toyDto.ImageDtos = imagesDto.ToList();
            return toyDto;

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

        public async Task UpdateToyAsync(ToyDto toyDto)
        {
            await _database.Toy.UpdateToyAsync(_mapper.Map<ToyDto, Toy>(toyDto));
            _database.Save();
        }

        public async Task<List<ImageDto>> GetImagesByToyIdAsync(Guid id)
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
    }
}

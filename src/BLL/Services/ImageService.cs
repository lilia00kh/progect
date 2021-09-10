using AutoMapper;
using BLL.EntitiesDTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;

        public ImageService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _database = unitOfWork;
        }

        public async Task CreateImageAsync(ImageDto imageDto)
        {
            await _database.Image.Create(_mapper.Map<ImageDto, Image>(imageDto));
            _database.Save();
        }

        public async Task DeleteImageByNameAsync(string imageName)
        {
            var images = (await _database.Image.FindByCondition(x => x.ImageName == imageName, trackChanges: false)).ToList();
            foreach (var i in images)
            {
                var imageAndGood = (await _database.ImageAndGood.FindByCondition(x => x.ImageId == i.Id, trackChanges: false)).FirstOrDefault();
                await _database.Image.Delete(i);
                await _database.ImageAndGood.Delete(imageAndGood);
            }
            _database.Save();
        }
    }
}

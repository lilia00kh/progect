using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PL.Models;

namespace PL.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class UploadController : ControllerBase
    {

        private readonly IImageService _imageService;

        public UploadController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<List<ImageModel>>> Upload([Required] List<IFormFile> images)
        {
            var result = new List<ImageModel>();

            if (images == null || images.Count == 0)
            {
                return BadRequest("No file is uploaded.");
            }

            foreach (var image in images)
            {
                
                var filePath = Path.Combine(@"wwwroot", image.FileName);
                new FileInfo(filePath).Directory?.Create();                
                await using var stream = new FileStream(filePath, FileMode.Create);
                await image.CopyToAsync(stream);
                
                result.Add(new ImageModel { ImageName = image.FileName, ImagePath = image.FileName });
            }

            return Ok(result);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(long.MaxValue)]
        public IActionResult Delete(string imagePath)
        {
            string Path = imagePath;
            FileInfo file = new FileInfo(Path);
            if (file.Exists)
            {
                file.Delete();
            }
            return Ok();
        }

        [HttpDelete("DeleteImageByNameFromDB")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(long.MaxValue)]
        public IActionResult DeleteImageFromDB(string imageName)
        {
            _imageService.DeleteImageByNameAsync(imageName);            
            return Ok();
        }

    }
    public class CertificateSubmissionResult
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
    }
}

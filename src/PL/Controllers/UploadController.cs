using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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


        // [HttpPost, DisableRequestSizeLimit]
        // public async Task<IActionResult> Upload()
        //{
        //     try
        //     {
        //         var files = Request.Form.Files;
        //         var folderName = Path.Combine("StaticFiles", "Images");
        //         var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        //         if (files.Any(f => f.Length == 0))
        //         {
        //             return BadRequest();
        //         }
        //         foreach (var file in files)
        //         {
        //             var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //             var fullPath = Path.Combine(pathToSave, fileName);
        //             var dbPath = Path.Combine(folderName, fileName); //you can add this path to a list and then return all dbPaths to the client if require
        //             using (var stream = new FileStream(fullPath, FileMode.Create))
        //             {
        //                 file.CopyTo(stream);
        //             }
        //         }
        //         return Ok("All the files are successfully uploaded.");
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, "Internal server error");
        //     }
        // }

    }
    public class CertificateSubmissionResult
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
    }
}

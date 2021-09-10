using BLL.EntitiesDTO;
using BLL.Infrastracture;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Models;
using System;
using System.Threading.Tasks;
using PL.Validations;
using PL.Mapping;

namespace PL.Controllers
{
    [Route("api/trees")]
    [ApiController]
    public class TreeController : ControllerBase
    {
        private readonly ITreeService _treeService;
        private readonly ILoggerManager _logger;

        public TreeController(ITreeService treeService)
        {
            _treeService = treeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrees(string treeType)
        {
            try
            {
                var treeTypeToLower = treeType.ToLower();
                if (!Validator.TreeTypeValidator(treeTypeToLower))
                    return BadRequest("Ялинок такого типу немає");
                var trees = await _treeService.GetAllTreesAsync();
                return Ok(Mapper.MapTreeModelList(trees, treeTypeToLower));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetTreeById(Guid id)
        {
            try
            {
                var treeDto = await _treeService.GetTreeByIdAsync(id);
                return Ok(Mapper.MapTreeModel(treeDto));
            }
            catch (CustomException)
            {
                return BadRequest("Tree does not exist");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateTree([FromBody] TreeModel treeModel)
        {
            try
            {
                if (!Validator.TreeTypeValidator(treeModel.TreeType))
                    return BadRequest("Ялинок такого типу немає");
                Validator.TreeValidator(treeModel);
                await _treeService.UpdateTreeAsync(new TreeDto()
                {
                    Id = treeModel.Id,
                    Name = treeModel.Name,
                    Description = treeModel.Description,
                    TreeType = treeModel.TreeType,
                    Color = treeModel.Color
                });

                foreach (var priceAndSizeModel in treeModel.PriceAndSizeModels)
                {
                    var id = await _treeService.CreateSizeAsync(new SizeDto() { NameOfSize = priceAndSizeModel.Size });
                    if (priceAndSizeModel.Id.ToString() == "00000000-0000-0000-0000-000000000000")
                    {
                        await _treeService.CreateTreeSizeAndPriceAsync(new TreeSizeAndPriceDto()
                        {
                            Price = priceAndSizeModel.Price,
                            SizeDtoId = id,
                            TreeDtoId = treeModel.Id
                        });
                    }

                }
                foreach (var image in treeModel.ImageModels)
                {
                    var imageExistInDB = await _treeService.ImageExistInDB(image.ImageName);
                    if (!imageExistInDB)
                    {
                        var id = await _treeService.CreateImageAsync(new ImageDto()
                        {
                            ImageName = image.ImageName,
                            ImagePath = image.ImagePath
                        });
                        await _treeService.CreateImageAndGoodAsync(new ImageAndGoodDto()
                        {
                            ImageId = id,
                            GoodId = treeModel.Id
                        });
                    }

                }
                return Ok();
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AddTree([FromBody] TreeModel treeModel)
        {
            try
            {
                if (!Validator.TreeTypeValidator(treeModel.TreeType))
                    return BadRequest("Ялинок такого типу немає");
                Validator.TreeValidator(treeModel);
                var treeId = await _treeService.CreateTreeAsync(new TreeDto()
                {
                    Name = treeModel.Name,
                    Description = treeModel.Description,
                    TreeType = treeModel.TreeType,
                    Color = treeModel.Color
                });

                foreach (var priceAndSizeModel in treeModel.PriceAndSizeModels)
                {
                    var id = await _treeService.CreateSizeAsync(new SizeDto() { NameOfSize = priceAndSizeModel.Size });
                    await _treeService.CreateTreeSizeAndPriceAsync(new TreeSizeAndPriceDto()
                    {
                        Price = priceAndSizeModel.Price,
                        SizeDtoId = id,
                        TreeDtoId = treeId
                    });
                }
                foreach (var image in treeModel.ImageModels)
                {
                    var id = await _treeService.CreateImageAsync(new ImageDto() { ImageName = image.ImageName, ImagePath = image.ImagePath });
                    await _treeService.CreateImageAndGoodAsync(new ImageAndGoodDto()
                    {
                        ImageId = id,
                        GoodId = treeId
                    });
                }
                return Ok();
            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("DeletePriceAndSizeById")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteTreePriceAndSizeById(Guid id)
        {
            try
            {
                await _treeService.DeleteTreePriceAndSizeAsync(id);
                return Ok();
            }
            catch (CustomException ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(BLL.Services.TreeService.DeleteTreeAsync)} action {ex}");
                return StatusCode(500, "Internal server error, you can`t delete tree price and size that does not exist");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(DeleteTree)} action {ex}");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpDelete("Delete")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteTree(Guid id)
        {
            try
            {
                await _treeService.DeleteTreeAsync(id);
                return Ok();
            }
            catch (CustomException ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(BLL.Services.TreeService.DeleteTreeAsync)} action {ex}");
                return StatusCode(500, "Internal server error, you can`t delete tree that doesn`t exist");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(DeleteTree)} action {ex}");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("SearchTreeByName")]
        public async Task<IActionResult> SearchTreeByName(string name, string treeType)
        {
            try
            {
                var treeTypeToLower = treeType.ToLower();
                if (!Validator.TreeTypeValidator(treeTypeToLower))
                    return BadRequest("Ялинок такого типу немає");
                var trees = await _treeService.SearchByName(name);
                if (trees == null)
                    return BadRequest("Ялинок з такою назвою немає.");
                return Ok(Mapper.MapTreeModelList(trees, treeTypeToLower));
            }
            catch (CustomException)
            {
                return BadRequest("Помилка");
            }
            catch (Exception)
            {
                return StatusCode(500, "Внутрішня серверна помилка");
            }
        }

        //    [HttpGet ("GetTreesByFilter")]
        //    public async Task<IActionResult> GetTreesByFilter(FilterDto filterDto,string treeType)
        //    {
        //        try
        //        {
        //            var treeTypeToLower = treeType.ToLower();
        //            if (!Validator.TreeTypeValidator(treeTypeToLower))
        //                return BadRequest("Ялинок такого типу немає");
        //            var trees = await _treeService.GetAllTreesByFilterAsync(filterDto);
        //            return Ok(Mapper.MapTreeModelList(trees, treeTypeToLower));
        //        }
        //        catch (Exception)
        //        {
        //            return StatusCode(500, "Internal server error");
        //        }
        //    }
        //}
    }
}

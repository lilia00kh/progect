using BLL.EntitiesDTO;
using BLL.Infrastracture;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PL.Validations;
using PL.Mapping;

namespace PL.Controllers
{
    [Route("api/toys")]
    [ApiController]
    public class ToyController : ControllerBase
    {
        private readonly IToyService _toyService;

        public ToyController(IToyService toyService)
        {
            _toyService = toyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetToys()
        {
            try
            {
                var toys = await _toyService.GetAllToysAsync();
                return Ok(Mapper.MapToyModelList(toys));
            }
            catch(CustomException)
            {
                return Ok(new List<ToyModel>());
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AddToy([FromBody] ToyModel toyModel)
        {
            try
            {
                Validator.ToyValidator(toyModel);
                var toyDto = new ToyDto()
                {
                    Id = toyModel.Id,
                    Name = toyModel.Name,
                    Description = toyModel.Description,
                    Price = toyModel.Price
                };
                var toyId = await _toyService.CreateToyAsync(toyDto);
                foreach (var image in toyModel.ImageModels)
                {
                    var id = await _toyService.CreateImageAsync(new ImageDto() { ImageName = image.ImageName, ImagePath = image.ImagePath });
                    await _toyService.CreateImageAndGoodAsync(new ImageAndGoodDto()
                    {
                        ImageId = id,
                        GoodId = toyId
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

        [HttpDelete("Delete")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteToy(Guid id)
        {
            try
            {
                await _toyService.DeleteToyAsync(id);
                return Ok();
            }
            catch (CustomException)
            {
                return StatusCode(500, "Internal server error, you can`t delete tree that doesn`t exist");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetToyById(Guid id)
        {
            try
            {
                var toyDto = await _toyService.GetToyByIdAsync(id);
                return Ok(Mapper.MapToyModel(toyDto));
            }
            catch (CustomException)
            {
                return BadRequest("Toy does not exist");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateToy([FromBody] ToyModel toyModel)
        {
            try
            {
                Validator.ToyValidator(toyModel);
                await _toyService.UpdateToyAsync(new ToyDto()
                {
                    Id = toyModel.Id,
                    Name = toyModel.Name,
                    Description = toyModel.Description,
                    Price = toyModel.Price
                });
                foreach (var image in toyModel.ImageModels)
                {
                    var imageExistInDB = await _toyService.ImageExistInDB(image.ImageName);
                    if (!imageExistInDB)
                    {
                        var id = await _toyService.CreateImageAsync(new ImageDto()
                        {
                            ImageName = image.ImageName,
                            ImagePath = image.ImagePath
                        });
                        await _toyService.CreateImageAndGoodAsync(new ImageAndGoodDto()
                        {
                            ImageId = id,
                            GoodId = toyModel.Id
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

        [HttpGet("SearchToyByName")]
        public async Task<IActionResult> SearchToyByName(string name)
        {
            try
            {
                var toys = await _toyService.SearchByName(name);
                if (toys == null)
                    return BadRequest("Прикрас з такою назвою немає.");
                return Ok(Mapper.MapToyModelList(toys));
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
        
    }
}

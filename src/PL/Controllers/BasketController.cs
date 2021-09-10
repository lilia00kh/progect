using BLL.EntitiesDTO;
using BLL.Infrastracture;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PL.Controllers
{
    [Route("api/baskets")]
    [ApiController]
    [Authorize]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost]
        public IActionResult Create(string name, int count)
        {
            string key = "details";
            string value = Request.Cookies[key];
            var detailsAboutGoodDto = new DetailsAboutGoodDto()
            {
                GoodId = new Guid(),
                Name = "kkkk",
                TypeOfGood = "tree",
                Count = count,
                Price = 2,
                Size = 2
            };
            string str = value 
                +"\n"
                + detailsAboutGoodDto.Id
                + detailsAboutGoodDto.Name
                + detailsAboutGoodDto.TypeOfGood
                + detailsAboutGoodDto.Count
                + detailsAboutGoodDto.Price
                + detailsAboutGoodDto.Size;
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Append(key, str, cookieOptions);
            return Ok();            
        }

        [HttpGet("GetCookie")]
        public IActionResult Read(string name)
        {
            string key = name;
            string value = Request.Cookies[key];
            return Ok(value);
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            try
            {
                var userName = User.Identity.Name;
                var basketDto = await _basketService.GetBasket(userName);
                var detailsAboutGoodDto = await _basketService.GetAllGoodsFromBasket(userName);
                List<TreeForBasketModel> trees = new List<TreeForBasketModel>();
                List<ToyForBasketModel> toys = new List<ToyForBasketModel>();
                foreach(var details  in detailsAboutGoodDto)
                {

                    var images = await GetImagesAsynk(details.GoodId);
                    if (details.TypeOfGood == "tree")
                        trees.Add(new TreeForBasketModel() {
                            Id = details.Id,
                            Name = details.Name,
                            Count = details.Count,
                            Size = details.Size,
                            Price = details.Price,
                            TreeId = details.GoodId,
                            ImageModels = images,
                            Color = details.Color
                        });
                    else
                    {
                        toys.Add(new ToyForBasketModel()
                        {
                            Id = details.Id,
                            Name = details.Name,
                            Count = details.Count,
                            Price = details.Price,
                            ToyId = details.GoodId,
                            ImageModels = images
                        });
                    }
                    
                }
                var basket = new BasketModel()
                {
                    Id = basketDto.Id,
                    UserName = basketDto.UserName,
                    Trees = trees,
                    Toys = toys
                };
                return Ok(basket);
            }
            catch (CustomException)
            {
                return StatusCode(200, "List of toys is empty");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        private async Task<List<ImageModel>> GetImagesAsynk(Guid goodId)
        {
            var imageDtos = await _basketService.GetImagesByGoodId(goodId);
            var images = new List<ImageModel>();
            foreach (var i in imageDtos)
            {
                images.Add(new ImageModel()
                {
                    ImageName = i.ImageName,
                    ImagePath = i.ImagePath
                });
            }
            return images;
        }

        [HttpPost("AddToyToBasket")]
        public async Task<IActionResult> AddToyToBasket(ToyForBasketModel toyForBasketModel)
        {
            try
            {
                var userName = User.Identity.Name;
                var detailsAboutGoodDto = new DetailsAboutGoodDto()
                {
                    GoodId = toyForBasketModel.ToyId,
                    Name = toyForBasketModel.Name,
                    TypeOfGood = "toy",
                    Count = toyForBasketModel.Count,
                    Price = toyForBasketModel.Price
                };
                await _basketService.AddToyToBasketAsync(userName, toyForBasketModel.ToyId, detailsAboutGoodDto);
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

        [HttpPost("AddTreeToBasket")]
        public async Task<IActionResult> AddTreeToBasket(TreeForBasketModel treeForBasketModel)
        {
            try
            {
                var userName = User.Identity.Name;
                var detailsAboutGoodDto = new DetailsAboutGoodDto()
                {
                    GoodId = treeForBasketModel.TreeId,
                    Name = treeForBasketModel.Name,
                    TypeOfGood = "tree",
                    Count = treeForBasketModel.Count,
                    Price = treeForBasketModel.Price,
                    Size = treeForBasketModel.Size,
                    Color = treeForBasketModel.Color
                };
                await _basketService.AddTreeToBasketAsync(userName, treeForBasketModel.TreeId, detailsAboutGoodDto);
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

        [HttpDelete("DeleteGoodFromBasket")]
        public async Task<IActionResult> DeleteGoodFromBasket(Guid detailsId)
        {
            try
            {
                await _basketService.DeleteGoodFromBasketAsync(detailsId);
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

    }
}

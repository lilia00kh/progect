using System;
using System.Threading.Tasks;
using BLL.EntitiesDTO;
using BLL.Interfaces;
using BLL.JwtFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.Infrastracture;
using PL.Models;
using System.Collections.Generic;
using PL.Mapping;

namespace PL.Controllers
{
    [Route("api/SearchAndRecomendationResponse")]
    [ApiController]
    public class SearchAndRecomendationResponseController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly JwtHandler _jwtHandler;
        private readonly ISearchAndRecomendationResponseService _searchAndRecomendationResponseService;

        public SearchAndRecomendationResponseController(ISearchAndRecomendationResponseService searchAndRecomendationResponseService, ILoggerManager logger, JwtHandler jwtHandler)
        {
            _logger = logger;
            _jwtHandler = jwtHandler;
            _searchAndRecomendationResponseService = searchAndRecomendationResponseService;
        }

        [HttpGet("SearchByName")]
        public async Task<IActionResult> SearchByName(string name)
        {
            try
            {
                var lowerName = name.ToLower();
                var searchResult = await _searchAndRecomendationResponseService.SearchByName(lowerName);
                if (searchResult == null)
                    return BadRequest("Товару з такою назвою немає.");
                return Ok(Mapper.MapSearchAndRecomendationResponseModel(searchResult));
            }
            catch (CustomException)
            {
                return Ok(new SearchAndRecomendationResponseModel());
            }
            catch (Exception)
            {
                return StatusCode(500, "Внутрішня серверна помилка");
            }
        }

        [HttpPost("addRecomendation")]
        public async Task<IActionResult> AddRecomendation(RecomendationDto recomendationDto)
        {
            try
            {
                await _searchAndRecomendationResponseService.CreateRecomendationAsync(recomendationDto);
                return Ok();
            }
            catch (CustomException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Внутрішня серверна помилка");
            }
        }
        [HttpDelete("deleteRecomendation")]
        public async Task<IActionResult> DeleteRecomendation(Guid id)
        {
            try
            {
                await _searchAndRecomendationResponseService.DeleteRecomendationByGoodId(id);
                return Ok();
            }
            catch (CustomException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Внутрішня серверна помилка");
            }
        }

        [HttpGet("getAllRecomendations")]
        public async Task<IActionResult> GetAllRecomendations()
        {
            try
            {
                return Ok(await _searchAndRecomendationResponseService.GetAllRecomendations());
            }
            catch (CustomException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Внутрішня серверна помилка");
            }
        }
    }
}

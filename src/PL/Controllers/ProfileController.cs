using System;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EntitiesDTO;
using BLL.Interfaces;
using BLL.JwtFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.Infrastracture;
using PL.Models;

namespace PL.Controllers
{
    [Route("api/profile")]
    [Authorize]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private IMapper _mapper;
        private readonly JwtHandler _jwtHandler;
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService, ILoggerManager logger, IMapper mapper, JwtHandler jwtHandler)
        {
            _logger = logger;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProfileModel, ProfileDto > ()
                .ForMember(p => p.FirstName, c => c.MapFrom(a => a.FirstName))
                .ForMember(p => p.LastName, c => c.MapFrom(a => a.LastName))
                .ForMember(p => p.Email, c => c.MapFrom(a => a.Email))
                .ReverseMap()
            ).CreateMapper();
            try
            {
                var userName = User.Identity.Name;
                var profileDto = await _profileService.GetProfileData(userName);
                var profileModel = _mapper.Map<ProfileModel>(profileDto);
                return Ok(profileModel);
            }
            catch (CustomException ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(_profileService.GetProfileData)} action {ex}");
                return StatusCode(200, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(_profileService.GetProfileData)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileModel profileModel)
        {
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProfileDto, ProfileModel>()
                .ForMember(p => p.FirstName, c => c.MapFrom(a => a.FirstName))
                .ForMember(p => p.LastName, c => c.MapFrom(a => a.LastName))
                .ForMember(p => p.Email, c => c.MapFrom(a => a.Email))
                .ReverseMap()
            ).CreateMapper();
            try
            {
                var profileDto = _mapper.Map<ProfileDto>(profileModel);
                await _profileService.UpdateProfileData(profileDto);
                //var profileModel = _mapper.Map<ProfileModel>(profileDto);
                return Ok();
            }
            catch (CustomException ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(_profileService.GetProfileData)} action {ex}");
                return StatusCode(200, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(_profileService.GetProfileData)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

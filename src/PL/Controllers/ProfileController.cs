using System;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EntitiesDTO;
using BLL.Interfaces;
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
        private IMapper _mapper;
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService, IMapper mapper)
        {
            _mapper = mapper;
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
                return StatusCode(200, ex.Message);
            }
            catch (Exception)
            {
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
                return Ok();
            }
            catch (CustomException ex)
            {
                return StatusCode(200, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

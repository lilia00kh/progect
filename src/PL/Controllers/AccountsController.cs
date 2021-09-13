using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EntitiesDTO;
using BLL.Infrastracture;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PL.Models;

namespace PL.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private IMapper _mapper;
        private readonly IAuthService _authService;

        public AccountsController(IAuthService authService,UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _authService = authService;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationModel userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserForRegistrationDto, UserForRegistrationModel>()
            .ForMember(p => p.FirstName, c => c.MapFrom(a => a.FirstName))
                .ForMember(p => p.LastName, c => c.MapFrom(a => a.LastName))
                .ForMember(p => p.Email, c => c.MapFrom(a => a.Email))
                .ForMember(p => p.Password, c => c.MapFrom(a=>a.Password))
                .ForMember(p => p.ClientUri, c => c.MapFrom(a=>a.ClientUri))
                .ReverseMap()
            ).CreateMapper();
            try
            {
                var user = _mapper.Map<UserForRegistrationDto>(userForRegistration);
                await _authService.Registration(user);
                return StatusCode(201);
            }
            catch (CustomException)
            {
                return BadRequest("Користувач вже зареєстрований");
            }

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationModel userForAuthentication)
        {
            if (userForAuthentication == null || !ModelState.IsValid)
                return BadRequest();

            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserForAuthenticationDto, UserForAuthenticationModel>()
            .ForMember(p => p.Email, c => c.MapFrom(a => a.Email))
            .ForMember(p => p.Password, c => c.MapFrom(a => a.Password))
            .ReverseMap()
            ).CreateMapper();
            var user = _mapper.Map<UserForAuthenticationDto>(userForAuthentication);
            var res = await _authService.Login(user);
            if (!res.IsAuthSuccessful)
                return BadRequest(res.ErrorMessage);
            return Ok(res);
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel forgotPasswordModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                await _authService.ForgotPasswordAsync(forgotPasswordModel.Email, forgotPasswordModel.ClientURI);
                return Ok();
            }
            catch(CustomException)
            { return BadRequest("Invalid Request"); }

        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (User.Identity.Name != null)
            {
                resetPasswordModel.Email = User.Identity.Name;
                resetPasswordModel.Token = await _authService.GeneratePasswordResetTokenAsync(resetPasswordModel.Email);
            }

            var resetPassResult = await _authService.ResetPasswordAsync(resetPasswordModel.Email, resetPasswordModel.Token,
                resetPasswordModel.Password);
            if (resetPassResult.Succeeded) return Ok();

            if (resetPasswordModel.Email == null)
            {
                return BadRequest();
            }
            var errors = resetPassResult.Errors.Select(e => e.Description);
            return BadRequest(new { Errors = errors });

        }

        [HttpGet("EmailConfirmation")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Invalid Email Confirmation Request");
            var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
            if (!confirmResult.Succeeded)
                return BadRequest("Invalid Email Confirmation Request");
            return Ok();
        }
    }
}
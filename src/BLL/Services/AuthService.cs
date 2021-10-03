using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EntitiesDTO;
using BLL.Infrastracture;
using BLL.Interfaces;
using BLL.JwtFeatures;
using BLL.Services.EmailService;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;


namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly JwtHandler _jwtHandler;
        private readonly IUnitOfWork _database;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(UserManager<User> userManager, IMapper mapper, JwtHandler jwtHandler, IUnitOfWork unitOfWork, IEmailSender emailSender, IHttpContextAccessor httpContextAccessor)
        {

            _mapper = mapper;
            _jwtHandler = jwtHandler;
            _database = unitOfWork;
            _database.User.userManager = userManager;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthentication)
        {
            var user = await _database.User.FindUserByEmailAsync(userForAuthentication.Email);
            if (user == null)
                return (new AuthResponseDto { ErrorMessage = "Користувач з таким логіном не зареєстрований" });
            if (!await _database.User.IsEmailConfirmedAsync(user))
                return new AuthResponseDto { ErrorMessage = "Email не підтверджений" };
            var ifPasswordIsCorrect = await _database.User.CheckUserPasswordAsync(user, userForAuthentication.Password);
            if (!ifPasswordIsCorrect)
                return (new AuthResponseDto { ErrorMessage = "Неправильний пароль " });
            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, await claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return (new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }

        public async Task Registration(UserForRegistrationDto userForRegistration)
        {
            string clientUrl = _httpContextAccessor.HttpContext.Request.Scheme
                + "://"
                + _httpContextAccessor.HttpContext.Request.Host.Value
                + "/authentication/emailconfirmation";
            userForRegistration.ClientUri = clientUrl;
            if ((await _database.User.FindUserByEmailAsync(userForRegistration.Email)) != null)
                throw new CustomException($"Користувач {userForRegistration.Email} не зареєстрований", "");
            var user = _mapper.Map<UserForRegistrationDto, User>(userForRegistration);
            var result = await _database.User.Registration(user, userForRegistration.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                throw new CustomException("Реєстрація не здійснена", "");
            }
            var token = await _database.User.GenerateEmailConfirmationTokenAsync(user);
            await SendEmailAsync("Будь ласка, підтвердіть email", userForRegistration.Email, userForRegistration.ClientUri, token);
            await _database.User.AddToRoleAsync(user);
            await _database.Basket.Create(new Basket() { UserName = user.UserName });
            _database.Save();
        }

        public async Task AddRoleToNewUserAsync(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<UserForRegistrationDto, User>(userForRegistration);
            await _database.User.AddRoleForNewUser(user);
        }

        public async Task SendEmailAsync(string mess, string email, string clientUri, string token)
        {

            var param = new Dictionary<string, string>
            {
                {"token", token },
                {"email", email }
            };

            var callback = QueryHelpers.AddQueryString(clientUri, param);
            var message = new Message(new string[] { email }, mess, callback, null);
            await _emailSender.SendEmailAsync(message);
        }

        public async Task ForgotPasswordAsync(string email, string clientUri)
        {
            string clientUrl = _httpContextAccessor.HttpContext.Request.Scheme
                + "://"
                + _httpContextAccessor.HttpContext.Request.Host.Value
                + "/authentication/resetpassword";
            clientUri = clientUrl;
            var user = await _database.User.FindUserByEmailAsync(email);
            if (user == null)
                throw new CustomException($"Користувач {email} не зареєстрований", "");
            var token = await _database.User.GeneratePasswordResetTokenAsync(user);
            await SendEmailAsync("Зміна паролю", email, clientUri, token);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _database.User.FindUserByEmailAsync(email);
            if (user == null)
                throw new CustomException($"Користувач {email} не зареєстрований", "");
            return await _database.User.GeneratePasswordResetTokenAsync(user);

        }
        public async Task<IdentityResult> ResetPasswordAsync(string email, string token, string password)
        {
            var user = await _database.User.FindUserByEmailAsync(email);
            if (user == null)
                throw new CustomException($"Користувач {email} не зареєстрований", "");
            var res = await _database.User.ResetPasswordAsync(user, token, password);
            _database.Save();
            return res;
        }
    }
}

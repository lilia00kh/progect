using System.Threading.Tasks;
using BLL.EntitiesDTO;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

namespace BLL.Interfaces
{
   public  interface IAuthService
    {
        Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthentication);
        Task Registration(UserForRegistrationDto userForRegistration);
        Task AddRoleToNewUserAsync(UserForRegistrationDto userForRegistration);
        Task ForgotPasswordAsync(string email, string clientUri);
        Task<string> GeneratePasswordResetTokenAsync(string email);
        Task<IdentityResult> ResetPasswordAsync(string email, string token, string password);
    }
}

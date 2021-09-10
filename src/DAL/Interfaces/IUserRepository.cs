using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        public UserManager<User> userManager { get; set; }
        Task<User> FindUserByEmailAsync(string email);
        Task<bool> CheckUserPasswordAsync(User user, string password);
        Task<IdentityResult> Registration(User entity, string password);
        Task AddRoleForNewUser(User entity);
        Task<string> GeneratePasswordResetTokenAsync(User user);
        Task<string> GenerateEmailConfirmationTokenAsync(User user);
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);
        Task<bool> IsEmailConfirmedAsync(User user);
        Task<IdentityResult> AddToRoleAsync(User entity);
        Task Update(User entity);
    }
}

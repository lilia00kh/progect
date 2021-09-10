using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Task = System.Threading.Tasks.Task;

namespace DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserManager<User> userManager { get; set; }
        public UserRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            return await userManager.FindByNameAsync(email);
        }
        public async Task<bool> CheckUserPasswordAsync(User user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<bool> IsEmailConfirmedAsync(User user)
        {
            return await userManager.IsEmailConfirmedAsync(user);
        }
        public void Create(User entity)
        {
            
        }
        public async Task AddRoleForNewUser(User entity)
        {

            await userManager.AddToRoleAsync(entity, "Viewer");
        }
        public async Task<IdentityResult> Registration(User entity, string password)
        {
            return await userManager.CreateAsync(entity, password);
        }
        public async Task<IdentityResult> AddToRoleAsync(User entity)
        {
            return await userManager.AddToRoleAsync(entity, "User");
        }

        public async Task<IdentityResult> AddToAdminRoleAsync(User entity)
        {
            return await userManager.AddToRoleAsync(entity, "Administrator");
        }


        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return await userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string password)
        {
            return await userManager.ResetPasswordAsync(user, token, password);
        }
        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> FindAll(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> FindByCondition(Expression<Func<User, bool>> expression, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public async Task Update(User entity)
        {
            var user = await FindUserByEmailAsync(entity.Email);
            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await userManager.GenerateEmailConfirmationTokenAsync(user);
        }
    }
}

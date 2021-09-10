using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EntitiesDTO;
using BLL.Interfaces;
using BLL.JwtFeatures;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;
        private readonly JwtHandler _jwtHandler;
        //private readonly IProfileService _profileService;
        public ProfileService(UserManager<User> userManager,/* IProfileService profileService,*/ IMapper mapper, JwtHandler jwtHandler, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _jwtHandler = jwtHandler;
            _database = unitOfWork;
            _database.User.userManager = userManager;
            //_profileService = profileService;
        }
        public async Task<ProfileDto> GetProfileData(string email)
        {
            var user = await _database.User.FindUserByEmailAsync(email);
            return new ProfileDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }
        public async Task UpdateProfileData(ProfileDto profileDto)
        {
            var user = new User {Email = profileDto.Email, FirstName = profileDto.FirstName, LastName = profileDto.LastName};
            await _database.User.Update(user);
            _database.Save();
        }
    }
}

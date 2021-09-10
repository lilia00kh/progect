using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.EntitiesDTO;

namespace BLL.Interfaces
{
    public interface IProfileService
    {
        Task UpdateProfileData(ProfileDto profileDto);
        Task<ProfileDto> GetProfileData(string email);
    }
}

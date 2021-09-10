using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace DAL.Configuration
{
    public class UserConfiguration2
    {
        public UserManager<User> userManager { get; set; }
        
    }
}
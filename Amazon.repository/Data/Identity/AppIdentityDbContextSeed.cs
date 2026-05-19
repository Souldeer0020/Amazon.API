using Amazon.core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.repository.Data.Identity
{
    public static class  AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            var user = new AppUser()
            {
                DisplayName = "MoahmedTarek",
                Email = "MohamedT@gmail.com",
                UserName = "MohamedT",
                PhoneNumber = "01011123221"
            };

            if (!userManager.Users.Any())
                await userManager.CreateAsync(user, "P@ssw0rdP@ssw0rd");
            
        }
    }
}

using Amazon.core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Amazon.API_s.Extensions
{
    public static class userManagerExtension
    {
        public static async Task<AppUser?> FindByEmailWithAddressAsync(this UserManager<AppUser> userManager, ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            return await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}

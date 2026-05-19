using Amazon.core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.service
{
    public interface ITokenService
    {
        public Task<string> CreateTokenAsync(AppUser appUser, UserManager<AppUser> userManager);
    }
}

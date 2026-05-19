using Amazon.core.Entities.Identity;
using Amazon.service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.core.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser appUser, UserManager<AppUser> userManager)
        {
            var authClaims = new List<Claim>() 
            { 
                new Claim(ClaimTypes.GivenName,appUser.UserName),
                new Claim(ClaimTypes.Email,appUser.Email)
            };

            var userRoles =await userManager.GetRolesAsync(appUser);
            foreach (var role in userRoles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:KEY"]));

            var Token = new JwtSecurityToken( // here we are creating the token object(not encoded)
                issuer: _configuration["JWT:ValidIssuer"], //start of public claims
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"])), //end of public claims
                claims: authClaims, //private claims
                signingCredentials:new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)

                );
            return new JwtSecurityTokenHandler().WriteToken(Token); // here we are creating token encoded
        }
    }
}

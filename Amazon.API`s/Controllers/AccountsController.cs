using Amazon.API_s.DTO_s;
using Amazon.API_s.Errors;
using Amazon.API_s.Extensions;
using Amazon.core.Entities.Identity;
using Amazon.service;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Amazon.API_s.Controllers
{

    public class AccountsController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AccountsController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IMapper mapper,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if(user is null) return Unauthorized(new ApiResponse(401,"Email never registered before"));
            var result =await _signInManager.CheckPasswordSignInAsync(user, login.Password,false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401,"Email or password is incorrect"));
            return Ok(new UserDto()
            {
                DisplayName=user.DisplayName,
                Email=user.Email,
                Token=await _tokenService.CreateTokenAsync(user,_userManager)
            });
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {
            if (CheckEmailExists(register.Email).Result.Value)
                return BadRequest(new ApiResponse(400, "Email is already in use"));

            var user = _mapper.Map<RegisterDto, AppUser>(register);
            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
        }

        [Authorize]
        [HttpGet("address")]

        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindByEmailWithAddressAsync(User);

            return Ok(_mapper.Map<Address, AddressDto>(user.Address));
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAdress(AddressDto addressDto)
        {
            var user =await _userManager.FindByEmailWithAddressAsync(User);

            var address = _mapper.Map<AddressDto, Address>(addressDto);

            address.Id = user.Address.Id ; // if we dont put this he will insert new row with new Id

            user.Address = address;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return Ok(address);
        }
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }
    }
}

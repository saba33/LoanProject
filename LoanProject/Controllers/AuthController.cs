using LoanProject.Data.Models;
using LoanProject.Services.Abstractions;
using LoanProject.Services.Models;
using LoanProject.Services.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IPasswordHasher _hasher;
        private readonly IUserService _userService;

        public AuthController(IPasswordHasher hasher, IUserService userService)
        {
            _hasher = hasher;
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginModel request)
        {
            // add loggers 
            var result = await _userService.LoginUser(request);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<LoginResponse>> Register(UserDto request)
        {
            // add loggers 
            var result = await _userService.RegisterUserAsync(request);
            return Ok(result);
        }
    }
}

using LoanProject.Services.Abstractions;
using LoanProject.Services.Models;
using LoanProject.Services.Models.User;
using LoanProject.Services.Models.User.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LoanProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginModel request)
        {
            var result = await _userService.LoginUser(request);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterResponse>> Register(UserDto request)
        {
            var result = await _userService.RegisterUserAsync(request);
            return Ok(result);
        }
    }
}

 
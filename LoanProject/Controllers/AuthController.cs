using LoanProject.Data.Models;
using LoanProject.Services.Abstractions;
using LoanProject.Services.Models;
using LoanProject.Web.Model;
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

        public static User user = new User();
        public AuthController(IPasswordHasher hasher, IUserService userService)
        {
            _hasher = hasher;
            _userService = userService;

        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(UserDto request)
        {
            // add loggers 
            var result = await _userService.RegisterUserAsync(request);
            return Ok(result);
        }
    }
}

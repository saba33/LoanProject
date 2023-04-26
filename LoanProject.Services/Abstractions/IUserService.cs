using LoanProject.Services.Models;
using LoanProject.Services.Models.User;
using LoanProject.Services.Models.User.Responses;

namespace LoanProject.Services.Abstractions
{
    public interface IUserService
    {
        Task<RegisterResponse> RegisterUserAsync(UserDto user);
        Task<LoginResponse> LoginUser(LoginModel request);
        Task<Dictionary<byte[], byte[]>> GetHashandSalt(string mail);
    }
}

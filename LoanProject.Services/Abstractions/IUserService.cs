using LoanProject.Services.Models;
using LoanProject.Services.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProject.Services.Abstractions
{
    public interface IUserService
    {
        Task<RegisterResponse> RegisterUserAsync(UserDto user);
        Task<LoginResponse> LoginUser(LoginModel request);
        Task<Dictionary<byte[], byte[]>> GetHashandSalt(string mail);
    }
}

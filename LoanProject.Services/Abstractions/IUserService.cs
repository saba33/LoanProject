using LoanProject.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProject.Services.Abstractions
{
    public interface IUserService
    {
        Task<string> RegisterUserAsync(UserDto user);
        Task<string> LoginUser(string email, string password);
    }
}

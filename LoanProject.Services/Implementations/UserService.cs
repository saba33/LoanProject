using LoanProject.Data.Models;
using LoanProject.Repository.Abstractions;
using LoanProject.Repository.Implementations;
using LoanProject.Services.Abstractions;
using LoanProject.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LoanProject.Services.Implementations
{

    public class UserService : IUserService
    {
        private readonly IPasswordHasher _hasher;
        private readonly ILoanServiceRepository<User> _repo;
        public UserService(IPasswordHasher hasher, ILoanServiceRepository<User> repo)
        {
            _hasher = hasher;
            _repo = repo;
        }
        //check for password check byte or string
        public async Task<string> LoginUser(string email, string password)
        {
            // add return types error types
            var users = await _repo.FindAsync(u => u.Email == email);
            var user = users.FirstOrDefault();

            //users == null or userss firstordefault() == null
            if (user == null)
            {
                return "Email or password is incorrect";
            }
            if (!_hasher.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return "Email or password is incorrect";
            }

            return user.IdNumber;
        }

        public async Task<string> RegisterUserAsync(UserDto request)
        {
            _hasher.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User()
            {
                Name = request.Name,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth,
                IdNumber = request.IdNumber,
                LastName = request.IdNumber,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _repo.AddAsync(user);
            return user.IdNumber;
        }
    }
}

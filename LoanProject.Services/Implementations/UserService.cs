using LoanProject.Data.Models;
using LoanProject.Repository.Abstractions;
using LoanProject.Repository.Implementations;
using LoanProject.Services.Abstractions;
using LoanProject.Services.Models;
using LoanProject.Services.Models.Enum;
using LoanProject.Services.Models.Responses;
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
        private readonly IJwtService _jwtService;
        public UserService(IPasswordHasher hasher, ILoanServiceRepository<User> repo, IJwtService jwtService)
        {
            _hasher = hasher;
            _repo = repo;
            _jwtService = jwtService;
        }

        public async Task<Dictionary<byte[], byte[]>> GetHashandSalt(string mail)
        {
            var result = new Dictionary<byte[], byte[]>();
            var user = _repo.FindAsync(p => p.Equals(mail)).Result.FirstOrDefault();
            var passHash = user.PasswordHash;
            var passSalt = user.PasswordSalt;
            result.Add(passHash, passSalt);
            return await Task.FromResult(result);
        }

        public async Task<LoginResponse> LoginUser(LoginModel request)
        {
            var users = await _repo.FindAsync(u => u.Email == request.Mail);
            var user = users.FirstOrDefault();
            var passInfo = GetHashandSalt(request.Mail).Result.FirstOrDefault();

            if (user == null)
            {
                return new LoginResponse { Message = "Email or password is incorrect" };
            }

            if (!_hasher.VerifyPasswordHash(request.Password, passInfo.Key, passInfo.Value))
            {
                return new LoginResponse { Message = "Email or password is incorrect" };
            }

            var token = _jwtService.GenerateToken(request.Mail);

            return new LoginResponse
            {
                Token = token,
                Message = "Token created successfully",
                StatusCode = StatusCodes.Success
            };
        }

        public async Task<RegisterResponse> RegisterUserAsync(UserDto request)
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
            return new RegisterResponse
            {
                Message = "Registration was Sucessfull",
                StatusCode = StatusCodes.Success
            };
        }
    }
}

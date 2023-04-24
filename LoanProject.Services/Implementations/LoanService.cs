using AutoMapper;
using LoanProject.Data.Models;
using LoanProject.Data.Models.Enums;
using LoanProject.Repository.Abstractions;
using LoanProject.Services.Abstractions;
using LoanProject.Services.Models.Enum;
using LoanProject.Services.Models.Loan.LoanServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProject.Services.Implementations
{
    public class LoanService : ILoanService
    {
        private readonly IPasswordHasher _hasher;
        private readonly IBaseRepository<Loan> _repo;
        private readonly IMapper _mapper;
        public LoanService(IPasswordHasher hasher, IJwtService jwtService, IMapper mapper, IBaseRepository<Loan> repo)
        {
            _hasher = hasher;
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<TakeLoanResponse> TakeLoan(TakeLoanRequestDto request, int userId)
        {
            var insertModel = _mapper.Map<Loan>(request);
            insertModel.UserId = userId;
            await _repo.AddAsync(insertModel);

            return new TakeLoanResponse
            {
                Message = "Loan request is sent",
                Status = LoanStatus.Pending,
                StatusCode = StatusCodes.Success
            };
        }

        public async Task<IEnumerable<Loan>> GetLoans()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<IEnumerable<Loan>>
    }
}

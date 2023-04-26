using AutoMapper;
using Azure.Core;
using LoanProject.Data.Models;
using LoanProject.Data.Models.Enums;
using LoanProject.Repository.Abstractions;
using LoanProject.Services.Abstractions;
using LoanProject.Services.Models;
using LoanProject.Services.Models.Enum;
using LoanProject.Services.Models.Loan.LoanServiceRequest;
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
        private readonly ILoanRepository _loanRepo;
        private readonly IMapper _mapper;
        public LoanService(IPasswordHasher hasher, IJwtService jwtService, IMapper mapper, IBaseRepository<Loan> repo, ILoanRepository loanRepo)
        {
            _hasher = hasher;
            _repo = repo;
            _mapper = mapper;
            _loanRepo = loanRepo;
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

        public async Task<IEnumerable<Loan>> GetLoanByUserIdAsync(int userId)
        {
            return await _loanRepo.GetLoansByUserIdAsync(x => x.UserId == userId);
        }
        public async Task<EditLoanResponse> EditLoanInfoAsync(UpdateLoanRequest request, int loanId)
        {
            var loan = await _loanRepo.GetByIdAsync(loanId);
            loan.loanType = request.loanType;
            loan.Amount = request.Amount;
            loan.TermInMonths = request.TermInMonths;

            _loanRepo.Update(loan);
            return new EditLoanResponse
            {
                Message = "Loan is Updated !",
                StatusCode = StatusCodes.Success,
                UserId = loan.UserId
            };

        }

        public async Task<CancelLoanResponse> CancelLoan(int LoanId)
        {
            var loan = await _loanRepo.GetByIdAsync(LoanId);
            _loanRepo.Remove(loan);
            return new CancelLoanResponse
            {
                LoanId = LoanId,
                Message = "Loan request is cancelled",
                StatusCode = StatusCodes.Success
            };
        }
    }
}

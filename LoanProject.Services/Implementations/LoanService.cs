using AutoMapper;
using LoanProject.Data.Models;
using LoanProject.Data.Models.Enums;
using LoanProject.Repository.Abstractions;
using LoanProject.Services.Abstractions;
using LoanProject.Services.Models.Loan.LoanServiceRequest;
using LoanProject.Services.Models.Loan.LoanServiceResponses;
using Microsoft.AspNetCore.Http;

namespace LoanProject.Services.Implementations
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepo;
        private readonly IMapper _mapper;
        public LoanService(IMapper mapper, ILoanRepository loanRepo)
        {
            _mapper = mapper;
            _loanRepo = loanRepo;
        }

        public async Task<TakeLoanResponse> TakeLoan(TakeLoanRequestDto request, int userId)
        {
            var insertModel = _mapper.Map<Loan>(request);
            insertModel.UserId = userId;
            await _loanRepo.AddAsync(insertModel);

            return new TakeLoanResponse
            {
                Message = "Loan request is sent",
                Status = LoanStatus.Forwarded.ToString(),
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<IEnumerable<Loan>> GetLoans()
        {
            return await _loanRepo.GetAllAsync();
        }

        public async Task<IEnumerable<Loan>> GetLoanByUserIdAsync(int userId)
        {
            return await _loanRepo.GetLoansByUserIdAsync(x => x.UserId == userId);
        }

        public async Task<EditLoanResponse> EditLoanInfoAsync(UpdateLoanRequest request, int loanId)
        {
            var loan = await _loanRepo.GetByIdAsync(loanId);

            if (loan != null)
            {
                if (loan.LoanStatus == LoanStatus.Pending)
                {
                    loan.loanType = request.loanType;
                    loan.Amount = request.Amount;
                    loan.TermInMonths = request.TermInMonths;

                    _loanRepo.Update(loan);
                    return new EditLoanResponse
                    {
                        Message = "Loan is Updated !",
                        StatusCode = StatusCodes.Status200OK,
                        UserId = loan.UserId
                    };
                }
                return new EditLoanResponse
                {
                    Message = "Your loan has already been assigned a status, so it cannot be edited",
                    StatusCode = StatusCodes.Status200OK,//.
                    UserId = loan.UserId
                };
            }

            return new EditLoanResponse
            {
                Message = "Loan have not found with this LoanId ! ",
                StatusCode = StatusCodes.Status400BadRequest,
                UserId = 0
            };
        }

        public async Task<CancelLoanResponse> CancelLoan(int loanId)
        {
            var loan = await _loanRepo.GetByIdAsync(loanId);

            if (loan != null)
            {
                _loanRepo.Remove(loan);
                return new CancelLoanResponse
                {
                    LoanId = loanId,
                    Message = "Loan request is cancelled",
                    StatusCode = StatusCodes.Status200OK
                };
            }

            return new CancelLoanResponse
            {
                LoanId = loanId,
                Message = "Loan could not be found with this LoanId",
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
    }
}

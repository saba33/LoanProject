using LoanProject.Data.Models.Enums;
using LoanProject.Repository.Abstractions;
using LoanProject.Services.Abstractions;
using LoanProject.Services.Models.CRM;
using Microsoft.AspNetCore.Http;

namespace LoanProject.Services.Implementations
{
    public class CRMService : ICRMService
    {
        private readonly ILoanRepository _loanRepository;
        public CRMService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }
        public async Task<ChangeLoanStatusResponse> ChangeStatus(ChangeStatusRequestModel request)
        {
            var loan = await _loanRepository.GetByIdAsync(request.LoanId);

            if (loan != null)
            {
                loan.LoanStatus = request.Status;
                return new ChangeLoanStatusResponse
                {
                    LoanId = request.LoanId,
                    Message = "Status is changed",
                    Status = loan.LoanStatus.ToString(),
                    StatusCode = StatusCodes.Status200OK
                };
            }
            return new ChangeLoanStatusResponse
            {
                LoanId = request.LoanId,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Loan have not been found with this Id",
                Status = LoanStatus.Unknown.ToString()
            };
        }
    }
}

using LoanProject.Data.Models;
using LoanProject.Services.Models.Loan.LoanServiceRequest;
using LoanProject.Services.Models.Loan.LoanServiceResponses;

namespace LoanProject.Services.Abstractions
{
    public interface ILoanService
    {
        Task<TakeLoanResponse> TakeLoan(TakeLoanRequestDto request, int userId);
        Task<IEnumerable<Loan>> GetLoans();
        Task<IEnumerable<Loan>> GetLoanByUserIdAsync(int userId);
        Task<EditLoanResponse> EditLoanInfoAsync(UpdateLoanRequest loan, int loanId);
        Task<CancelLoanResponse> CancelLoan(int LoanId);

    }
}

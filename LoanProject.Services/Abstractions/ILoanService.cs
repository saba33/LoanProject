using LoanProject.Services.Models.Loan.LoanServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProject.Services.Abstractions
{
    public interface ILoanService
    {
        Task<TakeLoanResponse> TakeLoan(TakeLoanRequestDto request, int userId);
    }
}

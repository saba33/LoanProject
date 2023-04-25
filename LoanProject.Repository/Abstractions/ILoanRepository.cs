using LoanProject.Data.Models;
using LoanProject.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LoanProject.Repository.Abstractions
{
    public interface ILoanRepository : IBaseRepository<Loan>
    {
        Task<IEnumerable<Loan>> GetLoansAsync(CancellationToken cancellationToken, Expression<Func<Loan, bool>> predicate, int userId);
        Task UpdateLoanStatusAsync(int loanId, LoanStatus status);
        Task<IEnumerable<Loan>> GetExistingLoansAsync();
    }
}

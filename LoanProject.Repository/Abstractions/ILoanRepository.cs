using LoanProject.Data.Models;
using LoanProject.Data.Models.Enums;
using System.Linq.Expressions;

namespace LoanProject.Repository.Abstractions
{
    public interface ILoanRepository : IBaseRepository<Loan>
    {
        Task<IEnumerable<Loan>> GetLoansByUserIdAsync(Expression<Func<Loan, bool>> predicate);
        Task UpdateLoanStatusAsync(int loanId, LoanStatus status);
        Task<IEnumerable<Loan>> GetAll();
    }
}

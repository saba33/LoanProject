using LoanProject.Data.DbContect;
using LoanProject.Data.Models;
using LoanProject.Data.Models.Enums;
using LoanProject.Repository.Abstractions;
using System.Linq.Expressions;

namespace LoanProject.Repository.Implementations
{
    public class LoanRepository : BaseRepository<Loan>, ILoanRepository
    {

        public LoanRepository(DatabaseContext loanContext) : base(loanContext)
        {

        }

        public async Task<IEnumerable<Loan>> GetAll()
        {
            return await base.GetAllAsync();
        }

        public async Task<IEnumerable<Loan>> GetLoansByUserIdAsync(Expression<Func<Loan, bool>> predicate)
        {
            return await base.FindAsync(predicate);
        }

        public async Task UpdateLoanStatusAsync(int loanId, LoanStatus status)
        {
            var loan = (await base.FindAsync(x => x.LoanId == loanId)).FirstOrDefault();
            if (loan?.LoanStatus != status)
            {
                loan.LoanStatus = status;
                base.Update(loan);
            }
        }

    }

}

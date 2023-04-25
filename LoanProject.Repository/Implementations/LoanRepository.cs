using LoanProject.Data.DbContect;
using LoanProject.Data.Models;
using LoanProject.Data.Models.Enums;
using LoanProject.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
            var loan = base.FindAsync(x => x.LoanId == loanId).Result.FirstOrDefault();
            if (loan != null && loan.LoanStatus != status)
            {
                loan.LoanStatus = status;
                base.Update(loan);
            }
        }

    }

}

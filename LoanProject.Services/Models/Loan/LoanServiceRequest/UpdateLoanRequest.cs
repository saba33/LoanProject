using LoanProject.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProject.Services.Models.Loan.LoanServiceRequest
{
    public class UpdateLoanRequest
    {
        public LoanType loanType { get; set; }
        public decimal Amount { get; set; }
        public int TermInMonths { get; set; }
    }
}

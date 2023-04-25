using LoanProject.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProject.Services.Models.Loan.LoanServiceResponses
{
    public class TakeLoanRequestDto
    {
        public LoanType loanType { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateIssued { get; set; }
        public int TermInMonths { get; set; }
        public decimal InterestRate { get; set; }
        public decimal DownPayment { get; set; }
        public decimal MonthlyPayment { get; set; }
        public LoanStatus LoanStatus { get; set; }
    }
}

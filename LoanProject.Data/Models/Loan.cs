using LoanProject.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProject.Data.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public LoanType loanType { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int TermInMonths { get; set; }
        public decimal InterestRate { get; set; }
        public decimal DownPayment { get; set; }
        public decimal MonthlyPayment { get; set; }
        public LoanStatus LoanStatus { get; set; }
    }
}

using LoanProject.Data.Models.Enums;

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
        public Currencies Currency  { get; set; }
        public decimal InterestRate { get; set; }
        public decimal MonthlyPayment { get; set; }
        public LoanStatus LoanStatus { get; set; }
    }
}

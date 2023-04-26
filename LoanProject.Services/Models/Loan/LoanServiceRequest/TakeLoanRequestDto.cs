using LoanProject.Data.Models.Enums;

namespace LoanProject.Services.Models.Loan.LoanServiceRequest
{
    public class TakeLoanRequestDto
    {
        public LoanType loanType { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateIssued { get; set; } = DateTime.Now;
        public Currencies Currency { get; set; }
        public int TermInMonths { get; set; }
        public decimal InterestRate { get; set; } = 8;
        public decimal DownPayment { get; set; }
        public decimal MonthlyPayment { get; set; }
        public LoanStatus LoanStatus { get; set; }
    }
}

using LoanProject.Data.Models.Enums;

namespace LoanProject.Services.Models.Loan.LoanServiceRequest
{
    public class UpdateLoanRequest
    {
        public LoanType loanType { get; set; }
        public decimal Amount { get; set; }
        public int TermInMonths { get; set; }
    }
}

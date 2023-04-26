namespace LoanProject.Services.Models.Loan.LoanServiceResponses
{
    public class GetUserLoansResponse : BaseResponse
    {
        public IEnumerable<LoanProject.Data.Models.Loan> Loans { get; set; }
    }
}

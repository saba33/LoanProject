using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProject.Services.Models.Loan.LoanServiceResponses
{
    public class CancelLoanResponse: BaseResponse
    {
        public int LoanId { get; set; }
    }
}

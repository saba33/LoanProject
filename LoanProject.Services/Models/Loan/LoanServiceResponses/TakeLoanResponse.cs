using LoanProject.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProject.Services.Models.Loan.LoanServiceResponses
{
    public class TakeLoanResponse : BaseResponse
    {
        public LoanStatus Status { get; set; }
    }
}

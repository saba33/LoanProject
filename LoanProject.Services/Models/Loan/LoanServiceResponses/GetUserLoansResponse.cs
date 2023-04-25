﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProject.Services.Models.Loan.LoanServiceResponses
{
    public class GetUserLoansResponse : BaseResponse
    {
        public IEnumerable<LoanProject.Data.Models.Loan> Loans { get; set; }
    }
}
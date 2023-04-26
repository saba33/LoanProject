using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProject.Services.Models.CRM
{
    public class ChangeLoanStatusResponse : BaseResponse
    {
        public int LoanId { get; set; }
        public string Status { get; set; }
    }
}

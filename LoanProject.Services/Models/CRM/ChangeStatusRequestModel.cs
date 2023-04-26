using LoanProject.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProject.Services.Models.CRM
{
    public class ChangeStatusRequestModel
    {
        public int LoanId { get; set; }
        public LoanStatus Status { get; set; }
    }
}

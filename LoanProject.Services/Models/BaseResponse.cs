using Azure.Core;
using LoanProject.Services.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProject.Services.Models
{
    public class BaseResponse
    {
        public StatusCodes StatusCode { get; set; }
        public string Message { get; set; }
    }
}

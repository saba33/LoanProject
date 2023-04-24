using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProject.Services.Models.Enum
{
    public enum StatusCodes
    {
        Success = 200,
        BadRequest = 400,
        Timeout = 408,
        Unauthorized = 401
    }
}

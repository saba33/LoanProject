using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProject.Services.Models.User.Responses
{
    public class RegisterResponse : BaseResponse
    {
        public string Token { get; set; }
    }
}

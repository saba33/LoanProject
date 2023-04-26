using Microsoft.AspNetCore.Http;

namespace LoanProject.Services.Models
{
    public class BaseResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}

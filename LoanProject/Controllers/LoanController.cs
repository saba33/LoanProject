using LoanProject.Data.Models.Enums;
using LoanProject.Services.Abstractions;
using LoanProject.Services.Models.Loan.LoanServiceResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace LoanProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;
        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [Authorize]
        [HttpPost("TakeLoan")]
        public async Task<ActionResult<TakeLoanResponse>> TakeLoan(TakeLoanRequestDto loanRequest)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.Name);
            
            
            if (userIdClaim == null)
            {
                return BadRequest(new TakeLoanResponse
                {
                    Message = "Invalid Token",
                    Status = LoanStatus.Unknown,
                    StatusCode = LoanProject.Services.Models.Enum.StatusCodes.Unauthorized
                });
            }
            
            var response = await _loanService.TakeLoan(loanRequest, int.Parse(userIdClaim.Value));
            return Ok(response);
        }

        [HttpGet("GetUserLoans")]
        [Authorize]
        public async Task<GetUserLoansResponse> GetUserLoansByUserId(int userId)
        {
            return 
        }

    }
}

using LoanProject.Data.Models.Enums;
using LoanProject.Services.Abstractions;
using LoanProject.Services.Models.Loan.LoanServiceRequest;
using LoanProject.Services.Models.Loan.LoanServiceResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            TakeLoanResponse response; 

            if (userIdClaim != null)
            {
                response = await _loanService.TakeLoan(loanRequest, int.Parse(userIdClaim.Value));
                return Ok(response);
            }

            return BadRequest(new TakeLoanResponse
            {
                Message = "Invalid Token",
                Status = LoanStatus.Unknown.ToString(),
                StatusCode = StatusCodes.Status401Unauthorized
            });
        }

        [Authorize]
        [HttpGet("GetUserLoans/{userId}")]
        public async Task<ActionResult<GetUserLoansResponse>> GetUserLoansByUserId(int userId)
        {
            var result = await _loanService.GetLoanByUserIdAsync(userId);
            var response = new GetUserLoansResponse
            {
                Loans = result.ToList(),
                Message = "Current Loans.",
                StatusCode = StatusCodes.Status200OK
            };

            return Ok(response);
        }

        [Authorize]
        [HttpPut("EditLoan/{loanId}")]
        public async Task<ActionResult<EditLoanResponse>> EditLoanInfo(UpdateLoanRequest request, int loanId)
        {
            var result = await _loanService.EditLoanInfoAsync(request, loanId);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("CancelLoan")]
        public async Task<ActionResult<CancelLoanResponse>> CancelLoan(int loanId)
        {
            var result = await _loanService.CancelLoan(loanId);
            return Ok(result);
        }

    }
}

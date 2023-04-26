using LoanProject.Data.Models.Enums;
using LoanProject.Services.Abstractions;
using LoanProject.Services.Models.Loan.LoanServiceRequest;
using LoanProject.Services.Models.Loan.LoanServiceResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
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
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);


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

        [Authorize]
        [HttpGet("GetUserLoans")]
        public async Task<ActionResult<GetUserLoansResponse>> GetUserLoansByUserId(int userId)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var result = await _loanService.GetLoanByUserIdAsync(int.Parse(userIdClaim.Value));
            var response = new GetUserLoansResponse
            {
                Loans = result.ToList(),
                Message = "Current Loans.",
                StatusCode = Services.Models.Enum.StatusCodes.Success
            };

            return Ok(response);
        }

        [Authorize]
        [HttpPut("EditLoan")]
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

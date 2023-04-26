using LoanProject.Data.Models.Enums;
using LoanProject.Services.Abstractions;
using LoanProject.Services.Models.CRM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LoanProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRMController : ControllerBase
    {
        private readonly ICRMService _service;
        public CRMController(ICRMService service)
        {
            _service = service;
        }
        [Authorize]
        [HttpPut("ChangeLoanStatus")]
        public async Task<ActionResult<ChangeLoanStatusResponse>> ChangeLoanStatus(ChangeStatusRequestModel request)
        {
            var role = HttpContext.User.FindFirst(ClaimTypes.Role);
            if (role.Value == "Admin")
            {
                var result = await _service.ChangeStatus(request);
                if (result.StatusCode == StatusCodes.Status200OK)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            return Unauthorized(new ChangeLoanStatusResponse
            {
                Message = "you are not authorized for this opperation",
                StatusCode = StatusCodes.Status401Unauthorized,
                Status = LoanStatus.Unknown.ToString()
            });
        }
    }
}

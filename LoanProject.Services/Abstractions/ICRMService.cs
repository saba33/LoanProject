using LoanProject.Services.Models.CRM;

namespace LoanProject.Services.Abstractions
{
    public interface ICRMService
    {
        Task<ChangeLoanStatusResponse> ChangeStatus(ChangeStatusRequestModel request);
    }
}

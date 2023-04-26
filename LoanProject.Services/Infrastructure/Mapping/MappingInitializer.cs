using AutoMapper;
using LoanProject.Data.Models;
using LoanProject.Services.Models.Loan.LoanServiceRequest;

namespace LoanProject.Services.Infrastructure.Mapping
{
    public class MappingInitializer : Profile
    {
        public MappingInitializer()
        {
            CreateMap<TakeLoanRequestDto, Loan>().ReverseMap();
            CreateMap<UpdateLoanRequest, Loan>().ReverseMap();
        }
    }
}

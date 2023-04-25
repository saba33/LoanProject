using AutoMapper;
using LoanProject.Data.Models;
using LoanProject.Services.Models.Loan.LoanServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProject.Services.Infrastructure.Mapping
{
    public class MappingInitializer : Profile
    {
        public MappingInitializer()
        {
            CreateMap<TakeLoanRequestDto, Loan>().ReverseMap();
        }
    }
}

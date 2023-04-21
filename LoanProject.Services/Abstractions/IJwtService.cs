using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProject.Services.Abstractions
{
    public interface IJwtService
    {
        string GenerateToken(string email);
    }
}

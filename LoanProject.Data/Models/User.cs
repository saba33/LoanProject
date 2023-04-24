using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProject.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string IdNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public List<Loan> Loans { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

    }
}

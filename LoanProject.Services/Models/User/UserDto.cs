using LoanProject.Services.Infrastructure.Attributes;

namespace LoanProject.Services.Models.User
{
    public class UserDto
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [EmailValidator(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string IdNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }
}

namespace LoanProject.Services.Abstractions
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string role);
    }
}

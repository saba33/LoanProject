using LoanProject.Services.Abstractions;
using LoanProject.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace LoanProject.Tests.Services
{
    [TestFixture]
    public class JwtServiceTests
    {
        private IJwtService _jwtService;
        private Mock<IConfiguration> _mockConfig;

        [SetUp]
        public void SetUp()
        {
            _mockConfig = new Mock<IConfiguration>();
            _mockConfig.Setup(x => x.GetSection("JWTConfiguration:Secret").Value).Returns("mysecretkey");
            _mockConfig.Setup(x => x.GetSection("JWTConfiguration:ExpirationInMinutes").Value).Returns("60");
            _jwtService = new JwtService(_mockConfig.Object);
        }

        [Test]
        public void GenerateToken_Should_Return_Jwt_Token()
        {
            var userId = "1";
            var token = _jwtService.GenerateToken(userId);
            Assert.IsNotNull(token);
            Assert.IsNotEmpty(token);
        }
    }
}
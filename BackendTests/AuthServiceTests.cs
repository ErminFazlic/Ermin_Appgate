using Backend.Model;
using Backend.Services;
using Backend;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace BackendTests
{
    public class AuthServiceTests
    {
        private readonly DbContextOptions<Db> _dbContextOptions;
        private readonly Mock<IConfiguration> _mockConfiguration;

        public AuthServiceTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<Db>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(config => config["Jwt:Key"]).Returns("TestKey1234567890");
            _mockConfiguration.Setup(config => config["Jwt:Issuer"]).Returns("TestIssuer");
            _mockConfiguration.Setup(config => config["Jwt:Audience"]).Returns("TestAudience");
            _mockConfiguration.Setup(config => config["Jwt:ExpiresInMinutes"]).Returns("30");
        }

        [Fact]
        public async void Register_ShouldAddUser_WhenUsernameIsUnique()
        {
            using var context = new Db(_dbContextOptions);
            var authService = new AuthService(context, _mockConfiguration.Object);

            var result = await authService.Register("testuser", "password123");

            Assert.True(result);
            Assert.Single(context.Users);
            Assert.Equal("testuser", context.Users.First().Username);
        }

        [Fact]
        public async void Register_ShouldReturnFalse_WhenUsernameAlreadyExists()
        {
            using var context = new Db(_dbContextOptions);
            context.Users.Add(new User("testuser", "password123"));
            context.SaveChanges();

            var authService = new AuthService(context, _mockConfiguration.Object);

            var result = await authService.Register("testuser", "newpassword");

            Assert.False(result);
            Assert.Single(context.Users);
        }
    }
}

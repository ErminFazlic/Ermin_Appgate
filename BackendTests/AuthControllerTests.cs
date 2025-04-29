using Backend.Controllers;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BackendTests
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _controller = new AuthController(_mockAuthService.Object);
        }

        #region Register

        [Fact]
        public async void Register_ReturnsOk_WhenRegistrationIsSuccessful()
        {
            var loginModel = new LoginModel("testuser", "password123");
            _mockAuthService
                .Setup(service => service.Register(loginModel.Username, loginModel.Password))
                .ReturnsAsync(true);

            var result = await _controller.Register(loginModel);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
        }

        [Fact]
        public async void Register_ReturnsBadRequest_WhenRegistrationFails()
        {
            var loginModel = new LoginModel("testuser", "password123");
            _mockAuthService
                .Setup(service => service.Register(loginModel.Username, loginModel.Password))
                .ReturnsAsync(false);

            var result = await _controller.Register(loginModel);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult);
        }

        #endregion
    }
}

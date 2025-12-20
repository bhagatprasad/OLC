using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OLC.Web.API.Controllers;
using OLC.Web.API.Manager;
using OLC.Web.API.Models;

namespace OLC.Web.API.Tests.Controllers
{


    [TestFixture]
    public class AccountControllerTests
    {
        private Mock<IAccountManager> _accountManagerMock;
        private AccountController _controller;

        [SetUp]
        public void Setup()
        {
            _accountManagerMock = new Mock<IAccountManager>();
            _controller = new AccountController(_accountManagerMock.Object);
        }

        #region RegisterUserAsync

        [Test]
        public async Task RegisterUserAsync_WhenPasswordIsNull_ReturnsBadRequest()
        {
            // Arrange
            var request = new UserRegistration
            {
                Password = null
            };

            // Act
            var result = await _controller.RegisterUserAsync(request);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task RegisterUserAsync_WhenValid_ReturnsOk()
        {
            // Arrange
            var request = new UserRegistration
            {
                Password = "Test@123",
                Email = "Test@gmail.com"
            };

            var expectedResponse = new RegistrationResult
            {
                Success = true,
                StatusCode = 201,
                Message = "Sucessfully registered"
            };

            _accountManagerMock.Setup(x => x.RegisterUserAsync(request)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.RegisterUserAsync(request);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(expectedResponse, okResult.Value);
        }

        [Test]
        public async Task RegisterUserAsync_WhenException_Returns500()
        {
            // Arrange
            var request = new UserRegistration { Password = "Test@123" };

            _accountManagerMock
                .Setup(x => x.RegisterUserAsync(request))
                .ThrowsAsync(new Exception());

            // Act
            var result = await _controller.RegisterUserAsync(request);

            // Assert
            var statusResult = result as StatusCodeResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusResult.StatusCode);
        }

        #endregion

        #region AuthenticateUserAsync

        [Test]
        public async Task AuthenticateUserAsync_WhenValid_ReturnsOk()
        {
            var request = new UserAuthentication();
            var response = new AuthResponse();

            _accountManagerMock
                .Setup(x => x.AuthenticateUserAsync(request))
                .ReturnsAsync(response);

            var result = await _controller.AuthenticateUserAsync(request);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task AuthenticateUserAsync_WhenException_Returns500()
        {
            var request = new UserAuthentication();

            _accountManagerMock
                .Setup(x => x.AuthenticateUserAsync(request))
                .ThrowsAsync(new Exception());

            var result = await _controller.AuthenticateUserAsync(request);

            var statusResult = result as StatusCodeResult;
            Assert.AreEqual(500, statusResult.StatusCode);
        }

        #endregion

        #region GenerateUserClaimsAsync

        [Test]
        public async Task GenarateUserClaimsAsync_ReturnsOk()
        {
            var request = new AuthResponse();
            var response = new ApplicationUser();

            _accountManagerMock
                .Setup(x => x.GenarateUserClaimsAsync(request))
                .ReturnsAsync(response);

            var result = await _controller.GenarateUserClaimsAsync(request);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        #endregion

        #region ForgotPasswordAsync

        [Test]
        public async Task ForgotPasswordAsync_ReturnsOk()
        {
            var request = new ForgotPassword();
            long response = 1;

            _accountManagerMock
                .Setup(x => x.ForgotPasswordAsync(request))
                .ReturnsAsync(response);

            var result = await _controller.ForgotPasswordAsync(request);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        #endregion

        #region ResetPasswordAsync

        [Test]
        public async Task ResetPasswordAsync_ReturnsOk()
        {
            var request = new ResetPassword();
            bool isReseted = true;

            _accountManagerMock
                .Setup(x => x.ResetPasswordAsync(request))
                .ReturnsAsync(isReseted);

            var result = await _controller.ResetPasswordAsync(request);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        #endregion

        #region ChangePasswordAsync

        [Test]
        public async Task ChangePasswordAsync_ReturnsOk()
        {
            var request = new ChangePassword();
            bool passwordChanged = true;


            _accountManagerMock
                .Setup(x => x.ChangePasswordAsync(request))
                .ReturnsAsync(passwordChanged);

            var result = await _controller.ChangePasswordAsync(request);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        #endregion

        #region ExternalLogin

        [Test]
        public async Task LoginOrRegisterExternalUserAsync_ReturnsOk()
        {
            var request = new ExternalUserInfo();
            var response = new AuthResponse();

            _accountManagerMock
                .Setup(x => x.LoginOrRegisterExternalUserAsync(request))
                .ReturnsAsync(response);

            var result = await _controller.LoginOrRegisterExternalUserAsync(request);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        #endregion
    }

}

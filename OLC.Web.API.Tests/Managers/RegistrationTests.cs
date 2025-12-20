using OLC.Web.API.Models;

namespace OLC.Web.API.Tests.Managers
{
    [TestFixture]
    public class RegistrationTests : AccountManagerTests
    {
        [Test]
        public async Task RegisterUserAsync_SuccessfulRegistration_ReturnsSuccess()
        {
            // Arrange
            var registration = new UserRegistration
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@test.com",
                Phone = "1234567890",
                Password = "Password123!",
                RoleId = 2
            };

            SetupStoredProcedureExecution(1); // Success code

            // Act
            var result = await _accountManager.RegisterUserAsync(registration);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("User registered successfully"));
            Assert.That(result.StatusCode, Is.EqualTo(1));
        }

        [Test]
        public async Task RegisterUserAsync_DuplicateEmail_ReturnsFailure()
        {
            // Arrange
            var registration = new UserRegistration
            {
                Email = "existing@test.com",
                Password = "Password123!",
                RoleId = 2
            };

            SetupStoredProcedureExecution(-1); // Email exists code

            // Act
            var result = await _accountManager.RegisterUserAsync(registration);

            // Assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Email already exists"));
            Assert.That(result.StatusCode, Is.EqualTo(-1));
        }

        private void SetupStoredProcedureExecution(int returnCode)
        {
            // Mock SQL command execution
            // Implementation depends on your mocking strategy
        }
    }
}

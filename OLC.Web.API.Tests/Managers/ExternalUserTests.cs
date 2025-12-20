using Moq;
using OLC.Web.API.Models;
using System.Data.SqlClient;

namespace OLC.Web.API.Tests.Managers
{
    [TestFixture]
    public class ExternalUserTests : AccountManagerTests
    {
        [Test]
        public async Task LoginOrRegisterExternalUserAsync_ExistingUser_ReturnsSuccess()
        {
            // Arrange
            var externalUser = new ExternalUserInfo
            {
                Email = "existing@test.com",
                Name = "John",
                Surname = "Doe"
            };

            //SetupUserDataTable("existing@test.com");

            // Act
            var result = await _accountManager.LoginOrRegisterExternalUserAsync(externalUser);

            // Assert
            Assert.That(result.ValidUser, Is.True);
            Assert.That(result.StatusCode, Is.EqualTo(1000));
            Assert.That(result.Email, Is.EqualTo("existing@test.com"));
        }

        [Test]
        public async Task LoginOrRegisterExternalUserAsync_NewUser_SuccessfulRegistration()
        {
            // Arrange
            var externalUser = new ExternalUserInfo
            {
                Email = "new@test.com",
                Name = "Jane",
                Surname = "Smith"
            };

            SetupStoredProcedureForExternalUser(1, 100L); // Success

            // Act
            var result = await _accountManager.LoginOrRegisterExternalUserAsync(externalUser);

            // Assert
            Assert.That(result.ValidUser, Is.True);
            Assert.That(result.StatusMessage, Is.EqualTo("Registration successful"));
        }

        [Test]
        public async Task LoginOrRegisterExternalUserAsync_DuplicateEmail_ReturnsError()
        {
            // Arrange
            var externalUser = new ExternalUserInfo
            {
                Email = "duplicate@test.com",
                Name = "Test"
            };

            SetupStoredProcedureForExternalUser(-1, 0L); // Duplicate email

            // Act
            var result = await _accountManager.LoginOrRegisterExternalUserAsync(externalUser);

            // Assert
            Assert.That(result.ValidUser, Is.False);
            Assert.That(result.StatusMessage, Is.EqualTo("Email already exists"));
            Assert.That(result.StatusCode, Is.EqualTo(1001));
        }

     
        private void SetupStoredProcedureForExternalUser(int statusCode, long userId)
        {
            // Mock SQL execution for external user stored procedure
        }
        private void SetupUserDataTable(long userId)
        {
            _mockDataTable.Rows.Clear();
            var row = _mockDataTable.NewRow();
            row["Id"] = userId;
            _mockDataTable.Rows.Add(row);
        }
    }
}

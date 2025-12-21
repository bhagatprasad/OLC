using OLC.Web.API.Models;

namespace OLC.Web.API.Tests.Managers
{
    [TestFixture]
    public class PasswordTests : AccountManagerTests
    {
        [Test]
        public async Task ForgotPasswordAsync_ValidUser_ReturnsUserId()
        {
            // Arrange
            var forgotPassword = new ForgotPassword { username = "existing@test.com" };
            SetupUserDataTable(1L);

            // Act
            var result = await _accountManager.ForgotPasswordAsync(forgotPassword);

            // Assert
            Assert.That(result, Is.EqualTo(1L));
        }

        [Test]
        public async Task ForgotPasswordAsync_InvalidUser_ReturnsZero()
        {
            // Arrange
            var forgotPassword = new ForgotPassword { username = "nonexistent@test.com" };
            SetupEmptyDataTable();

            // Act
            var result = await _accountManager.ForgotPasswordAsync(forgotPassword);

            // Assert
            Assert.That(result, Is.EqualTo(0L));
        }

        [Test]
        public async Task ResetPasswordAsync_ValidRequest_ReturnsTrue()
        {
            // Arrange
            var resetPassword = new ResetPassword
            {
                UserId = 1L,
                Password = "NewPassword123!"
            };

            // Act
            var result = await _accountManager.ResetPasswordAsync(resetPassword);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task ResetPasswordAsync_InvalidRequest_ReturnsFalse()
        {
            // Arrange
            var resetPassword = new ResetPassword
            {
                UserId = null,
                Password = "NewPassword123!"
            };

            // Act
            var result = await _accountManager.ResetPasswordAsync(resetPassword);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task ChangePasswordAsync_ValidRequest_ReturnsTrue()
        {
            // Arrange
            var changePassword = new ChangePassword
            {
                UserId = 1L,
                Password = "NewPassword123!"
            };

            // Act
            var result = await _accountManager.ChangePasswordAsync(changePassword);

            // Assert
            Assert.That(result, Is.True);
        }

        private void SetupUserDataTable(long userId)
        {
            _mockDataTable.Rows.Clear();
            var row = _mockDataTable.NewRow();
            row["Id"] = userId;
            _mockDataTable.Rows.Add(row);
        }

        private void SetupEmptyDataTable()
        {
            _mockDataTable.Rows.Clear();
        }
    }
}

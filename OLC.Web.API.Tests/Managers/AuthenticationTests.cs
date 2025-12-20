using Moq;
using OLC.Web.API.Helpers;
using OLC.Web.API.Models;
using OLC.Web.Sms.Service;

namespace OLC.Web.API.Tests.Managers
{
    [TestFixture]
    public class AuthenticationTests : AccountManagerTests
    {
        [Test]
        public async Task AuthenticateUserAsync_InvalidUser_ReturnsInvalidUserResponse()
        {
            // Arrange
            var authRequest = new UserAuthentication
            {
                username = "nonexistent@test.com",
                password = "password123"
            };

            // Act
            var result = await _accountManager.AuthenticateUserAsync(authRequest);

            // Assert
            Assert.That(result.ValidUser, Is.False);
            Assert.That(result.ValidPassword, Is.False);
            Assert.That(result.StatusMessage, Is.EqualTo("Invalid user, please check and re-submit for login"));
        }

        [Test]
        public async Task AuthenticateUserAsync_BlockedUser_ReturnsBlockedResponse()
        {
            // Arrange
            var authRequest = new UserAuthentication
            {
                username = "blocked@test.com",
                password = "password123"
            };

            SetupUserDataTable(true, false, true, false);

            // Act
            var result = await _accountManager.AuthenticateUserAsync(authRequest);

            // Assert
            Assert.That(result.ValidUser, Is.False);
            Assert.That(result.IsActive, Is.False);
            Assert.That(result.StatusMessage, Is.EqualTo("User blocked, Please contact admin"));
            _mockSmsSubscriber.Verify(x => x.SendSmsAsync(It.IsAny<SmsRequest>()), Times.Once);
        }

        [Test]
        public async Task AuthenticateUserAsync_InactiveUser_ReturnsInactiveResponse()
        {
            // Arrange
            var authRequest = new UserAuthentication
            {
                username = "inactive@test.com",
                password = "password123"
            };

            SetupUserDataTable(false, false, false, false);

            // Act
            var result = await _accountManager.AuthenticateUserAsync(authRequest);

            // Assert
            Assert.That(result.ValidUser, Is.False);
            Assert.That(result.IsActive, Is.False);
            Assert.That(result.StatusMessage, Is.EqualTo("User Inactive, Please contact admin"));
        }

        [Test]
        public async Task AuthenticateUserAsync_ValidCredentials_ReturnsSuccessResponse()
        {
            // Arrange
            var authRequest = new UserAuthentication
            {
                username = "valid@test.com",
                password = "correctPassword"
            };


            var hashSalt = HashSalt.GenerateSaltedHash("correctPassword");

            var salt = hashSalt.Salt;
            var hash = hashSalt.Hash;

            SetupUserDataTable(false, true, false, false, hash, salt);

            // Act
            var result = await _accountManager.AuthenticateUserAsync(authRequest);

            // Assert
            Assert.That(result.ValidUser, Is.True);
            Assert.That(result.ValidPassword, Is.True);
            Assert.That(result.IsActive, Is.True);
            Assert.That(result.Email, Is.EqualTo("valid@test.com"));
        }

        [Test]
        public async Task AuthenticateUserAsync_InvalidPassword_ReturnsIncorrectPassword()
        {
            // Arrange
            var authRequest = new UserAuthentication
            {
                username = "valid@test.com",
                password = "wrongPassword"
            };

            var hashSalt = HashSalt.GenerateSaltedHash("correctPassword");

            var salt = hashSalt.Salt;
            var hash = hashSalt.Hash;

            SetupUserDataTable(false, true, false, false, hash, salt);

            // Act
            var result = await _accountManager.AuthenticateUserAsync(authRequest);

            // Assert
            Assert.That(result.ValidUser, Is.True);
            Assert.That(result.ValidPassword, Is.False);
            Assert.That(result.StatusMessage, Is.EqualTo("Incorrect password"));
        }

        [Test]
        public async Task AuthenticateUserAsync_ExternalUser_ReturnsGoogleAuthMessage()
        {
            // Arrange
            var authRequest = new UserAuthentication
            {
                username = "external@test.com",
                password = "password123"
            };

            SetupUserDataTable(false, true, true, true); // IsExternalUser = true

            // Act
            var result = await _accountManager.AuthenticateUserAsync(authRequest);

            // Assert
            Assert.That(result.ValidUser, Is.True);
            Assert.That(result.StatusMessage, Contains.Substring("Google authentication"));
        }

        [Test]
        public async Task AuthenticateUserAsync_NoPasswordHash_ReturnsResetPasswordMessage()
        {
            // Arrange
            var authRequest = new UserAuthentication
            {
                username = "nohash@test.com",
                password = "password123"
            };

            SetupUserDataTable(false, true, false, false, null, null); // No hash/salt

            // Act
            var result = await _accountManager.AuthenticateUserAsync(authRequest);

            // Assert
            Assert.That(result.ValidUser, Is.True);
            Assert.That(result.StatusMessage, Contains.Substring("reset your password"));
        }

        private void SetupUserDataTable(bool isBlocked, bool isActive, bool isExternal, bool hasHashSalt,
                                        string hash = null, string salt = null)
        {
            _mockDataTable.Clear();
            _mockDataTable.Columns.Add("Id", typeof(long));
            _mockDataTable.Columns.Add("FirstName", typeof(string));
            _mockDataTable.Columns.Add("LastName", typeof(string));
            _mockDataTable.Columns.Add("Email", typeof(string));
            _mockDataTable.Columns.Add("Phone", typeof(string));
            _mockDataTable.Columns.Add("PasswordHash", typeof(string));
            _mockDataTable.Columns.Add("PasswordSalt", typeof(string));
            _mockDataTable.Columns.Add("RoleId", typeof(long));
            _mockDataTable.Columns.Add("IsBlocked", typeof(bool));
            _mockDataTable.Columns.Add("IsActive", typeof(bool));
            _mockDataTable.Columns.Add("IsExternalUser", typeof(bool));

            var row = _mockDataTable.NewRow();
            row["Id"] = 1L;
            row["FirstName"] = "Test";
            row["LastName"] = "User";
            row["Email"] = "test@test.com";
            row["Phone"] = "1234567890";
            row["PasswordHash"] = hasHashSalt ? hash : DBNull.Value;
            row["PasswordSalt"] = hasHashSalt ? salt : DBNull.Value;
            row["RoleId"] = 2L;
            row["IsBlocked"] = isBlocked;
            row["IsActive"] = isActive;
            row["IsExternalUser"] = isExternal;
            _mockDataTable.Rows.Add(row);
        }
    }
}

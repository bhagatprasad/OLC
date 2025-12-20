using Moq;
using OLC.Web.API.Models;

namespace OLC.Web.API.Tests.Managers
{
    [TestFixture]
    public class ClaimsGenerationTests : AccountManagerTests
    {
        [Test]
        public async Task GenarateUserClaimsAsync_ValidUser_ReturnsClaims()
        {
            // Arrange
            var authResponse = new AuthResponse
            {
                Email = "test@test.com",
                ValidUser = true
            };

            SetupUserDataTable(1L, "John", "Doe", "test@test.com");
            SetupUserAccounts();

            // Act
            var result = await _accountManager.GenarateUserClaimsAsync(authResponse);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Email, Is.EqualTo("test@test.com"));
            Assert.That(result.FirstName, Is.EqualTo("John"));
            Assert.That(result.LastName, Is.EqualTo("Doe"));
        }

        [Test]
        public async Task GenarateUserClaimsAsync_InvalidUser_ReturnsEmptyClaims()
        {
            // Arrange
            var authResponse = new AuthResponse
            {
                Email = "invalid@test.com",
                ValidUser = false
            };

            //SetupEmptyDataTable();

            // Act
            var result = await _accountManager.GenarateUserClaimsAsync(authResponse);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Email, Is.Null);
            Assert.That(result.FirstName, Is.Null);
        }

        [Test]
        public async Task GenarateUserClaimsAsync_UserWithKyc_IncludesKycStatus()
        {
            // Arrange
            var authResponse = new AuthResponse
            {
                Email = "kycuser@test.com",
                ValidUser = true
            };

            SetupUserDataTable(1L, "KYC", "User", "kycuser@test.com");

            var userAccounts = new List<UserAccount>
        {
            new UserAccount { Id = 1L, KycStatus = "Approved" }
        };

            _mockUserManager
                .Setup(x => x.GetUserAccountsAsync())
                .ReturnsAsync(userAccounts);

            // Act
            var result = await _accountManager.GenarateUserClaimsAsync(authResponse);

            // Assert
            Assert.That(result.KycStatus, Is.EqualTo("Approved"));
        }

        private void SetupUserDataTable(long id, string firstName, string lastName, string email)
        {
            _mockDataTable.Rows.Clear();
            var row = _mockDataTable.NewRow();
            row["Id"] = id;
            row["FirstName"] = firstName;
            row["LastName"] = lastName;
            row["Email"] = email;
            row["Phone"] = "1234567890";
            row["RoleId"] = 2L;
            row["IsBlocked"] = false;
            row["IsActive"] = true;
            row["IsApproved"] = true;
            _mockDataTable.Rows.Add(row);
        }

        private void SetupUserAccounts()
        {
            var userAccounts = new List<UserAccount>();
            _mockUserManager
                .Setup(x => x.GetUserAccountsAsync())
                .ReturnsAsync(userAccounts);
        }
    }
}

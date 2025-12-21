using Microsoft.Extensions.Configuration;
using Moq;
using OLC.Web.API.Manager;
using OLC.Web.Sms.Service;
using System.Data;
using System.Data.SqlClient;


namespace OLC.Web.API.Tests.Managers
{
   
    [TestFixture]
    public class AccountManagerTests :IDisposable
    {
        public Mock<IConfiguration> _mockConfiguration;
        public Mock<IUserManager> _mockUserManager;
        public Mock<ISmsSubscriber> _mockSmsSubscriber;
        public Mock<SqlConnection> _mockSqlConnection;
        public Mock<SqlCommand> _mockSqlCommand;
        public Mock<SqlDataAdapter> _mockSqlDataAdapter;
        public AccountManager _accountManager;
        public DataTable _mockDataTable;
        public const string ConnectionString = "Server=test;Database=test;Trusted_Connection=True;";

        [SetUp]
        public void Setup()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockUserManager = new Mock<IUserManager>();
            _mockSmsSubscriber = new Mock<ISmsSubscriber>();
            _mockSqlConnection = new Mock<SqlConnection>();
            _mockSqlCommand = new Mock<SqlCommand>();
            _mockSqlDataAdapter = new Mock<SqlDataAdapter>();

            _mockConfiguration
                .Setup(x => x.GetConnectionString("DefaultConnection"))
                .Returns(ConnectionString);

            _accountManager = new AccountManager(
                _mockConfiguration.Object,
                _mockUserManager.Object,
                _mockSmsSubscriber.Object
            );

            _mockDataTable = new DataTable();
        }

        public void Dispose()
        {
            _mockSqlConnection = null;
            _mockSqlCommand = null;
            _mockSqlDataAdapter = null;
            _mockConfiguration = null;
            _mockUserManager = null;
            _mockSmsSubscriber = null;
            _accountManager = null;
            _mockDataTable = null;
        }
    }
}

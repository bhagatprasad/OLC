using OLC.Web.API.Helpers;
using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Manager
{
    public class AccountManager : IAccountManager
    {
        private readonly string connectionString;
        public AccountManager(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<AuthResponse> AuthenticateUserAsync(UserAuthentication userAuthentication)
        {

            AuthResponse authResponse = new AuthResponse();

            var user = await GetUserDetailsByUserName(userAuthentication.username);

            if (user == null)
            {
                authResponse.Email = string.Empty;
                authResponse.StatusMessage = "Invalid user, please check and re-submit for login";
                authResponse.StatusCode = 1000;
                authResponse.IsActive = false;
                authResponse.ValidUser = false;
                authResponse.ValidPassword = false;
            }

            if (user != null)
            {
                if (user.IsBlocked.Value == true)
                {
                    authResponse.Email = string.Empty;
                    authResponse.StatusMessage = "User blocked, Please contact admin";
                    authResponse.StatusCode = 1000;
                    authResponse.IsActive = false;
                    authResponse.ValidUser = false;
                    authResponse.ValidPassword = false;
                }
                else if (user.IsActive.Value == false)
                {
                    authResponse.Email = string.Empty;
                    authResponse.StatusMessage = "User Inactive, Please contact admin";
                    authResponse.StatusCode = 1000;
                    authResponse.IsActive = false;
                    authResponse.ValidUser = false;
                    authResponse.ValidPassword = false;
                }
                else if (user.IsActive.Value == true)
                {
                    if (!string.IsNullOrEmpty(userAuthentication.password))
                    {
                        bool isValidPassword = HashSalt.VerifyPassword(userAuthentication.password, user.PasswordHash, user.PasswordSalt);

                        if (isValidPassword)
                        {
                            authResponse.Email = user.Email;
                            authResponse.StatusMessage = "Active user";
                            authResponse.StatusCode = 1000;
                            authResponse.IsActive = true;
                            authResponse.ValidUser = true;
                            authResponse.ValidPassword = true;
                        }
                        else
                        {
                            authResponse.Email = string.Empty;
                            authResponse.StatusMessage = "Incorrect password";
                            authResponse.StatusCode = 1000;
                            authResponse.IsActive = true;
                            authResponse.ValidUser = true;
                            authResponse.ValidPassword = false;
                        }
                    }
                }
            }
            return authResponse;
        }

        public async Task<ApplicationUser> GenarateUserClaimsAsync(AuthResponse authResponse)
        {


            ApplicationUser applicationUser = new ApplicationUser();


            var user = await GetUserDetailsByUserName(authResponse.Email);

            if (user != null)
            {
                applicationUser.Id = user.Id;
                applicationUser.FirstName = user.FirstName;
                applicationUser.LastName = user.LastName;
                applicationUser.Email = user.Email;
                applicationUser.RoleId = user.RoleId;
                applicationUser.IsBlocked = user.IsBlocked;
                applicationUser.IsActive = user.IsActive;
                applicationUser.IsBlocked = user.IsBlocked;
                applicationUser.IsActive = user.IsActive;
                applicationUser.IsApproved = user.IsApproved;
                applicationUser.ApprovedBy = user.ApprovedBy;
                applicationUser.ApprovedOn = user.ApprovedOn;
                applicationUser.CreatedBy = user.CreatedBy;
                applicationUser.CreatedOn = user.CreatedOn;
                applicationUser.ModifiedBy = user.ModifiedBy;
                applicationUser.ModifiedOn = user.ModifiedOn;
                applicationUser.LastPasswordChangedOn = user.LastPasswordChangedOn;
            }

            return applicationUser;
        }

        public async Task<bool> RegisterUserAsync(UserRegistration userRegistration)
        {
            var hashSalt = HashSalt.GenerateSaltedHash(userRegistration.Password);

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspRegisterUser]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@firstName", userRegistration.FirstName);

            sqlCommand.Parameters.AddWithValue("@lastName", userRegistration.LastName);

            sqlCommand.Parameters.AddWithValue("@email", userRegistration.Email);

            sqlCommand.Parameters.AddWithValue("@phone", userRegistration.Phone);

            sqlCommand.Parameters.AddWithValue("@passwordHash", hashSalt.Hash);

            sqlCommand.Parameters.AddWithValue("@passwordSalt", hashSalt.Salt);

            sqlCommand.Parameters.AddWithValue("@roleId", userRegistration.RoleId);

            sqlCommand.ExecuteNonQuery();

            connection.Close();

            return true;
        }


        private async Task<User> GetUserDetailsByUserName(string userName)
        {
            User user = null;

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[uspGetUserByUserName]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@userName", userName);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    user = new User();

                    user.Id = Convert.ToInt64(item["Id"]);

                    user.FirstName = item["FirstName"] != DBNull.Value ? item["FirstName"].ToString() : null;

                    user.LastName = item["LastName"] != DBNull.Value ? item["LastName"].ToString() : null;

                    user.Email = item["Email"] != DBNull.Value ? item["Email"].ToString() : null;

                    user.Phone = item["Phone"] != DBNull.Value ? item["Phone"].ToString() : null;

                    user.PasswordHash = item["PasswordHash"] != DBNull.Value ? item["PasswordHash"].ToString() : null;

                    user.PasswordSalt = item["PasswordSalt"] != DBNull.Value ? item["PasswordSalt"].ToString() : null;

                    user.RoleId = item["RoleId"] != DBNull.Value ? Convert.ToInt64(item["RoleId"]) : null;

                    user.LastPasswordChangedOn = item["LastPasswordChangedOn"] != DBNull.Value ? (DateTimeOffset?)item["LastPasswordChangedOn"] : null;

                    user.IsBlocked = item["IsBlocked"] != DBNull.Value ? (bool?)item["IsBlocked"] : null;

                    user.IsApproved = item["IsApproved"] != DBNull.Value ? (bool?)item["IsApproved"] : null;

                    user.ApprovedBy = item["ApprovedBy"] != DBNull.Value ? Convert.ToInt64(item["ApprovedBy"]) : null;

                    user.ApprovedOn = item["ApprovedOn"] != DBNull.Value ? (DateTimeOffset?)item["ApprovedOn"] : null;

                    user.CreatedBy = item["CreatedBy"] != DBNull.Value ? Convert.ToInt64(item["CreatedBy"]) : null;

                    user.CreatedOn = item["CreatedOn"] != DBNull.Value ? (DateTimeOffset?)item["CreatedOn"] : null;

                    user.ModifiedBy = item["ModifiedBy"] != DBNull.Value ? Convert.ToInt64(item["ModifiedBy"]) : null;

                    user.ModifiedOn = item["ModifiedOn"] != DBNull.Value ? (DateTimeOffset?)item["ModifiedOn"] : null;

                    user.IsActive = item["IsActive"] != DBNull.Value ? (bool?)item["IsActive"] : null;
                }
            }
            return user;
        }
    }
}

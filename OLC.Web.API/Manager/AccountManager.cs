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
                applicationUser.Phone = user.Phone;
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

        public async Task<RegistrationResult> RegisterUserAsync(UserRegistration userRegistration)
        {
            try
            {
                var hashSalt = HashSalt.GenerateSaltedHash(userRegistration.Password);

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[uspRegisterUser]", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    // Add input parameters
                    sqlCommand.Parameters.AddWithValue("@firstName", userRegistration.FirstName ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@lastName", userRegistration.LastName ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@email", userRegistration.Email);
                    sqlCommand.Parameters.AddWithValue("@phone", userRegistration.Phone ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@passwordHash", hashSalt.Hash);
                    sqlCommand.Parameters.AddWithValue("@passwordSalt", hashSalt.Salt);
                    sqlCommand.Parameters.AddWithValue("@roleId", userRegistration.RoleId);

                    // Add output parameter for return code
                    var returnParameter = sqlCommand.Parameters.Add("@ReturnVal", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    await connection.OpenAsync();
                    await sqlCommand.ExecuteNonQueryAsync();

                    // Get the return value from stored procedure
                    int returnCode = (int)returnParameter.Value;

                    return new RegistrationResult
                    {
                        Success = returnCode == 1,
                        Message = GetErrorMessage(returnCode),
                        StatusCode = returnCode
                    };
                }
            }
            catch (Exception ex)
            {
                return new RegistrationResult
                {
                    Success = false,
                    Message = $"Registration failed: {ex.Message}",
                    StatusCode = -99
                };
            }
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
        // comment
        public async Task<long> ForgotPasswordAsync(ForgotPassword forgotPassword)
        {
            var user = await GetUserDetailsByUserName(forgotPassword.username);

            if (user != null)
                return user.Id;

            return 0;
        }

        public async Task<bool> ResetPasswordAsync(ResetPassword resetPassword)
        {
            if (resetPassword != null)
            {
                if (resetPassword.UserId.HasValue && !string.IsNullOrEmpty(resetPassword.Password))
                {
                    var hashSalt = HashSalt.GenerateSaltedHash(resetPassword.Password);

                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("[dbo].[uspResetPassword]", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@userId", resetPassword.UserId);
                    sqlCommand.Parameters.AddWithValue("@passwordHash", hashSalt.Hash);
                    sqlCommand.Parameters.AddWithValue("@passwordSalt", hashSalt.Salt);
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();

                    return true;
                }
                return false;
            }
            return false;
        }

        public async Task<AuthResponse> LoginOrRegisterExternalUserAsync(ExternalUserInfo externalUserInfo)
        {
            var authResponse = new AuthResponse();

            try
            {
                // Check if user already exists
                var existingUser = await GetUserDetailsByUserName(externalUserInfo.Email);
                if (existingUser != null)
                {
                    return new AuthResponse
                    {
                        Email = existingUser.Email,
                        StatusMessage = "Login successful",
                        StatusCode = 1000,
                        IsActive = true,
                        ValidUser = true,
                        ValidPassword = true
                    };
                }

                // Register new user
                using (var connection = new SqlConnection(connectionString))
                using (var sqlCommand = new SqlCommand("[dbo].[uspRegisterExternalUser]", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    // Add only the parameters that the stored procedure expects
                    sqlCommand.Parameters.AddWithValue("@firstName", externalUserInfo.Name ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@lastName", externalUserInfo.Surname ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@email", externalUserInfo.Email);
                    sqlCommand.Parameters.AddWithValue("@roleId", 2);

                    await connection.OpenAsync();

                    // Use ExecuteScalar to get the UserId returned by SELECT SCOPE_IDENTITY()
                    var result = await sqlCommand.ExecuteScalarAsync();

                    if (result != null && result != DBNull.Value)
                    {
                        long newUserId = Convert.ToInt64(result);

                        // Get the newly created user
                        var newUser = await GetUserDetailsByUserName(externalUserInfo.Email);

                        return new AuthResponse
                        {
                            Email = externalUserInfo.Email,
                            StatusMessage = "Registration successful",
                            StatusCode = 1000,
                            IsActive = true,
                            ValidUser = true,
                            ValidPassword = true,
                        };
                    }
                    else
                    {
                        return new AuthResponse
                        {
                            Email = string.Empty,
                            StatusMessage = "Registration failed",
                            StatusCode = 1001,
                            IsActive = false,
                            ValidUser = false,
                            ValidPassword = false
                        };
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL-specific exceptions
                return new AuthResponse
                {
                    Email = string.Empty,
                    StatusMessage = $"Database error: {sqlEx.Message}",
                    StatusCode = 1002,
                    IsActive = false,
                    ValidUser = false,
                    ValidPassword = false
                };
            }
            catch (Exception ex)
            {
                return new AuthResponse
                {
                    Email = string.Empty,
                    StatusMessage = $"Authentication failed: {ex.Message}",
                    StatusCode = 1002,
                    IsActive = false,
                    ValidUser = false,
                    ValidPassword = false
                };
            }
        }

        private string GetErrorMessage(int returnCode)
        {
            return returnCode switch
            {
                1 => "User registered successfully",
                -1 => "Email already exists",
                -2 => "Phone number already exists",
                -3 => "Both email and phone already exist",
                _ => "Registration failed"
            };
        }
    }
}
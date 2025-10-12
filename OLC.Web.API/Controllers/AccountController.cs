using Microsoft.AspNetCore.Mvc;
using OLC.Web.API.Helpers;
using OLC.Web.API.Models;
using System.Data;
using System.Data.SqlClient;

namespace OLC.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        string connectionString = "Data Source=104.243.32.43;Initial Catalog=dev_olc_db;Persist Security Info=True;User ID=olc_db_usr;Password=DubaiDutyFree@2025;Encrypt=False;Trust Server Certificate=True";
        public AccountController()
        {

        }
        [HttpPost]
        [Route("RegisterUserAsync")]
        public async Task<IActionResult> RegisterUserAsync(UserRegistration userRegistration)
        {
            if (string.IsNullOrEmpty(userRegistration.Password))
                return BadRequest("Password feild required");

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

            return Ok("Successfully registration complated");

        }
    }
}

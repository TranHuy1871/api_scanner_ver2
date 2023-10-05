using ApiCamScanner.DTOs;
using ApiCamScanner.MySql;
using Microsoft.AspNetCore.Mvc;

namespace ApiCamScanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ExecuteSql _execute;

        public UsersController(IConfiguration config)
        {
            _config = config;
            _execute = new ExecuteSql(_config.GetConnectionString("MyConnection"));
        }

        [HttpPost("Register")]
        public IActionResult Register(User user)
        {
            try
            {
                if (IsUserExists(user))
                {
                    return BadRequest("User already exists");
                }

                string insertUserQuery = "INSERT INTO users (username, password, email, phoneNumber) VALUES (@username, @password, @email, @phoneNumber); SELECT LAST_INSERT_ID()";
                var parameters = new { username = user.Username, password = user.Password, email = user.Email, phoneNumber = user.PhoneNumber };
                _execute.ExecuteScalar<int>(insertUserQuery, parameters);

                return Ok("Create user is successful");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private bool IsUserExists(User user)
        {
            var selectUserQuery = "SELECT COUNT(*) FROM users WHERE username = @username OR email = @email";
            var parameters = new { username = user.Username, email = user.Email };
            bool check = _execute.CheckExists(selectUserQuery, parameters);
            return check;
        }


        [HttpPost("Login")]
        public IActionResult Login([FromBody] User user)
        {
            try
            {
                var authenticatedUser = AuthenticateUser(user);

                if (authenticatedUser != null)
                    return Ok(authenticatedUser);
                else
                    return NotFound("Invalid username or password");
            }
            catch (Exception)
            {
                return BadRequest("An error occurred");
            }
        }

        private User AuthenticateUser(User user)
        {
            string selectUserQuery = "SELECT * FROM users WHERE username = @username AND password = @password";
            var parameters = new { username = user.Username, password = user.Password };
            return _execute.CheckAuthenticateUser<User>(selectUserQuery, parameters);
        }

        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordRequest request)
        {
            try
            {
                string updatePasswordQuery = "UPDATE users SET password = @newPassword WHERE username = @username AND password = @currentPassword";
                var parameters = new { newPassword = request.NewPassword, username = request.Username, currentPassword = request.CurrentPassword };
                bool isPasswordChanged = _execute.Execute(updatePasswordQuery, parameters);

                if (isPasswordChanged)
                    return Ok("Password changed successfully");
                else
                    return BadRequest("Failed to change password");
            }
            catch (Exception)
            {
                return BadRequest("An error occurred");
            }
        }
    }
}

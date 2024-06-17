using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserFarmerCRUDapis.Models;

namespace UserFarmerCRUDapis.Controllers
{
    [Route("v1/api/kisan_mitar/user")]
    [ApiController]
    public class LoginController : ControllerBase

    {
        private readonly KisanmitraKisanContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginController> _logger;

        public LoginController(KisanmitraKisanContext context, IConfiguration configuration, ILogger<LoginController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger; 
        }
        public class UserLoginModel
        {
            public string? UserEmail { get; set; }
            public string? UserPassword { get; set; }
        }



        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(TbUser login)
        {
            var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                _logger.LogWarning($"{timestamp} - Invalid login request: login object or credentials are missing.");
                return BadRequest(new { success = false, message = "Invalid login request" });

            }

            var validUser = await AuthenticateUser(login);

            if (validUser == null)
            {
                _logger.LogWarning($"{timestamp} - Login attempt failed for user {login.Email}: Invalid email or password.");
                return Unauthorized(new { success = false, message = "Invalid email or password" });
            }

            var authUser = await AuthorizeUser(validUser);

            if (authUser == null)
            {
                _logger.LogError($"{timestamp} - Error while generating token for user {login.Email}.");
                return Unauthorized(new { status = 500, success = false, message = "Error while generating token!" });
            }

            return Ok(authUser);
        }

        private async Task<TbUser> AuthenticateUser(TbUser login)
        {
            var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                var emailParam = new SqlParameter("@email", login.Email);
                var passwordParam = new SqlParameter("@password", login.Password);

                var users = await _context.TbUsers
                    .FromSqlRaw("EXEC sp_LoginUser @email, @password", emailParam, passwordParam)
                    .ToListAsync();

                var user = users.FirstOrDefault();
                if (user == null || string.IsNullOrEmpty(user.UserId))
                {
                    _logger.LogWarning($"{timestamp} - Authentication failed for user {login.Email}: No matching user found.");
                    return null;
                }

                _logger.LogInformation($"{timestamp} - User {login.Email} authenticated successfully.");
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{timestamp} - Authentication exception for user {login.Email}: {ex.Message}");
                return null;
            }
        }


        private async Task<IActionResult> AuthorizeUser(TbUser validUser)
        {
            var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            var claims = new[]
         {
             new Claim(ClaimTypes.Name, validUser?.UserName ?? ""),
             new Claim("UserId", validUser?.UserId ?? ""),
             new Claim("Email", validUser?.Email ?? ""),
             new Claim("RoleId", validUser?.RoleId ?? "")
         };

            var token = await GenerateJwtToken(claims);
            string message;

            switch (validUser?.RoleId)
            {
                case "RA_001":
                    message = "Login successful As Admin";
                    break;
                case "RF_001":
                    message = "Login successful As Farmer";
                    break;
                case "RC_001":
                    message = "Login successful As Consultant";
                    break;
                default:
                    message = "Login successful";
                    break;
            }

            _logger.LogInformation($"{timestamp} - User {validUser.Email} authorized successfully with role {validUser.RoleId}.");
            return Ok(new { status = 200, success = true, message, accessToken = token, validUser });

        }



        private async Task<string> GenerateJwtToken(IEnumerable<Claim> claims)
        {
            var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = creds,
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"]
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var writtenToken = tokenHandler.WriteToken(token);

                _logger.LogInformation($"{timestamp} - JWT token generated successfully.");
                return writtenToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{timestamp} - Error generating JWT token: " + ex.Message);
                throw;
            }
        }


    }
}

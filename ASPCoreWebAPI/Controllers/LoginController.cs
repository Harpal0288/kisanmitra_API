using ASPCoreWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASPCoreWebAPI.Controllers
{
	[Route("v1/api/kisan_mitra")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private IConfiguration _config;
		private readonly KisanmitraKisanContext dbContext;
		private readonly ILogger _logger;

		public LoginController(ILogger<LoginController> logger,IConfiguration configuration, KisanmitraKisanContext dbContext)
		{
			_logger = logger;
			_config = configuration;
			this.dbContext = dbContext;
		}

		private TbUser AuthenticateUsers(TbUser user)
		{
			TbUser? user_ = null;

			try
			{
				var email = new SqlParameter("email", user.Email);
				var password = new SqlParameter("password", user.Password);

				var data = dbContext.TbUsers.FromSqlRaw("EXEC sp_GetEmailAndPassword @email, @password", email, password).ToList();

				_logger.LogInformation(data.First().UserName + " Authenticated");

				user_ = data.First();

			}
			catch(InvalidOperationException)
			{
				_logger.LogInformation("Invalid email and password");
			}
			catch (Exception ex)
			{
				_logger.LogInformation(ex.Message);
			}

			return user_;
		}

		private string GenerateToken(TbUser user)
		{
			try
			{
				var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
				var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
				var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.UserName??""),
				new Claim("userId", user.UserId??""),
				new Claim("email", user.Email??""),
				new Claim(ClaimTypes.Role, user.RoleId??"")
			};

				var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims,
					expires: DateTime.Now.AddMinutes(1), signingCredentials: credentials);

				_logger.LogInformation("Token is Generated!!");

				return new JwtSecurityTokenHandler().WriteToken(token);
			}
			catch(Exception ex)
			{
				_logger.LogInformation(ex.Message);
				return ex.Message;
			}
			
		}

		[AllowAnonymous]
		[HttpPost("login")]
		public IActionResult Login(TbUser user)
		{
			IActionResult response = Unauthorized();
			var user_ = AuthenticateUsers(user);
			if (user_ != null)
			{
                var token = GenerateToken(user_);
                response = Ok(new { token = token });
            }
			return response;
		}

		[Authorize]
		[HttpGet("get_user_by_email_and_password")]
		public async Task<ActionResult<TbUser>> getData(TbUser user)
		{
			try
			{
				if (user.Email == null)
				{
					return BadRequest("Email ID is required.");
				}
				if (user.Password == null)
				{
					return BadRequest("Password is required.");
				}
				if (dbContext.TbUsers == null)
				{
					return NotFound("The resource TbUser is not available.");
				}
				var emailParam = new SqlParameter("@email", user.Email);
				var passwordParam = new SqlParameter("@password", user.Password);

				var data = await dbContext.TbUsers.FromSqlRaw("EXEC sp_GetEmailAndPassword @email, @password", emailParam, passwordParam).ToListAsync();

				if (data == null)
				{
					return NotFound("No matching data is found.");
				}

				return Ok(data);
			}
			catch(InvalidOperationException)
			{
				return NotFound("Invalid Credentials");
			}
			catch (Exception e)
			{
				return Problem(detail: e.Message);
			}
		}
	}
}

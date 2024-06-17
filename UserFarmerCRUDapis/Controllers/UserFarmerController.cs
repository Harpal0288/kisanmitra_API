using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserFarmerCRUDapis.Models;

namespace UserFarmerCRUDapis.Controllers
{
    [Route("v1/api/kisan_mitar/user")]
    [ApiController]
    public class UserFarmerController : ControllerBase
    {
        private readonly KisanmitraKisanContext _context;
        private readonly ILogger<UserFarmerController> _logger;

        public UserFarmerController(KisanmitraKisanContext context, ILogger<UserFarmerController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public class UserFarmerDTO
        {
            public TbUser? User { get; set; }
            public TbFarmer? Farmer { get; set; }
        }

        [HttpPost("insert_user")]
        public async Task<IActionResult> PostTbUserAndFarmer(UserFarmerDTO userFarmerDTO)
        {
            var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            if (userFarmerDTO.User == null)
            {
                _logger.LogWarning($"{timestamp} - Invalid User details: All fields are required!.");
                return BadRequest(new { success = false, message = "User details are required" });
            }
            if (userFarmerDTO.Farmer == null)
            {
                _logger.LogWarning($"{timestamp} - Invalid Farmer details: All fields are required!.");
                return BadRequest(new { success = false, message = "Farmer details are required" });
            }

            try
            {
                var errorMessageParam = new SqlParameter("@error_message", SqlDbType.NVarChar, 1000)
                {
                    Direction = ParameterDirection.Output
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_SingleInsertUserAndFarmer @user_name, @aadhar_number, @user_email, @phone_number, @user_address, @user_password, @role_id, @user_inserted_by, @user_updated_by, @farm_size, @farm_location, @pin_code, @irrigation_method, @soil_type, @farming_experience, @membership_status, @membership_expiry, @language_preference, @farmer_inserted_by, @farmer_updated_by, @error_message OUTPUT",
                    new SqlParameter("@user_name", userFarmerDTO.User.UserName),
                    new SqlParameter("@aadhar_number", userFarmerDTO.User.AadharNumber),
                    new SqlParameter("@user_email", userFarmerDTO.User.Email),
                    new SqlParameter("@phone_number", userFarmerDTO.User.PhoneNumber),
                    new SqlParameter("@user_address", userFarmerDTO.User.Address),
                    new SqlParameter("@user_password", userFarmerDTO.User.Password),
                    new SqlParameter("@role_id", userFarmerDTO.User.RoleId),
                    new SqlParameter("@user_inserted_by", userFarmerDTO.User.InsertedBy),
                    new SqlParameter("@user_updated_by", userFarmerDTO.User.UpdatedBy),
                    new SqlParameter("@farm_size", userFarmerDTO.Farmer.FarmSize),
                    new SqlParameter("@farm_location", userFarmerDTO.Farmer.FarmLocation),
                    new SqlParameter("@pin_code", userFarmerDTO.Farmer.PinCode),
                    new SqlParameter("@irrigation_method", userFarmerDTO.Farmer.IrrigationMethod),
                    new SqlParameter("@soil_type", userFarmerDTO.Farmer.SoilType),
                    new SqlParameter("@farming_experience", userFarmerDTO.Farmer.FarmingExperience),
                    new SqlParameter("@membership_status", userFarmerDTO.Farmer.MembershipStatus),
                    new SqlParameter("@membership_expiry", userFarmerDTO.Farmer.MembershipExpiry),
                    new SqlParameter("@language_preference", userFarmerDTO.Farmer.LanguagePreference),
                    new SqlParameter("@farmer_inserted_by", userFarmerDTO.Farmer.InsertedBy),
                    new SqlParameter("@farmer_updated_by", userFarmerDTO.Farmer.UpdatedBy),
                    errorMessageParam
                );

                var errorMessage = errorMessageParam.Value != DBNull.Value ? (string)errorMessageParam.Value : null;
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    _logger.LogWarning($"{timestamp} - SQL Error: ${errorMessage}.");
                    return BadRequest(new { staus = 500, success = false, message = errorMessage });
                }

                _logger.LogInformation($"{timestamp} - User {userFarmerDTO.User.Email} created successfully");
                return Ok(new { status = 200, success = true, message = "User and farmer created successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{timestamp} - Creation error for User {userFarmerDTO.User.Email}: {ex.Message}");
                return StatusCode(500, new { success = false, message = "An error occurred while creating the user and farmer.", error = ex.Message });
            }
        }




        public class BulkUserFarmerDTO
        {
            public List<UserFarmerDTO>? UserFarmerList { get; set; }
        }


        [HttpPost("bulk_insert_users")]
        public async Task<IActionResult> BulkInsertUsers(BulkUserFarmerDTO bulkUserFarmerDTO)
        {
            if (bulkUserFarmerDTO == null || bulkUserFarmerDTO.UserFarmerList == null || !bulkUserFarmerDTO.UserFarmerList.Any())
            {
                return BadRequest(new { success = false, message = "User and Farmer details are required" });
            }

            var errorMessages = new List<string>();

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var userFarmerDTO in bulkUserFarmerDTO.UserFarmerList)
                    {
                        var errorMessageParam = new SqlParameter("@error_message", SqlDbType.NVarChar, 1000)
                        {
                            Direction = ParameterDirection.Output
                        };

                        await _context.Database.ExecuteSqlRawAsync(
                            "EXEC sp_SingleInsertUserAndFarmer @user_name, @aadhar_number, @user_email, @phone_number, @user_address, @user_password, @role_id, @user_inserted_by, @user_updated_by, @farm_size, @farm_location, @pin_code, @irrigation_method, @soil_type, @farming_experience, @membership_status, @membership_expiry, @language_preference, @farmer_inserted_by, @farmer_updated_by, @error_message OUTPUT",
                            new SqlParameter("@user_name", userFarmerDTO.User.UserName),
                            new SqlParameter("@aadhar_number", userFarmerDTO.User.AadharNumber),
                            new SqlParameter("@user_email", userFarmerDTO.User.Email),
                            new SqlParameter("@phone_number", userFarmerDTO.User.PhoneNumber),
                            new SqlParameter("@user_address", userFarmerDTO.User.Address),
                            new SqlParameter("@user_password", userFarmerDTO.User.Password),
                            new SqlParameter("@role_id", userFarmerDTO.User.RoleId),
                            new SqlParameter("@user_inserted_by", userFarmerDTO.User.InsertedBy),
                            new SqlParameter("@user_updated_by", userFarmerDTO.User.UpdatedBy),
                            new SqlParameter("@farm_size", userFarmerDTO.Farmer.FarmSize),
                            new SqlParameter("@farm_location", userFarmerDTO.Farmer.FarmLocation),
                            new SqlParameter("@pin_code", userFarmerDTO.Farmer.PinCode),
                            new SqlParameter("@irrigation_method", userFarmerDTO.Farmer.IrrigationMethod),
                            new SqlParameter("@soil_type", userFarmerDTO.Farmer.SoilType),
                            new SqlParameter("@farming_experience", userFarmerDTO.Farmer.FarmingExperience),
                            new SqlParameter("@membership_status", userFarmerDTO.Farmer.MembershipStatus),
                            new SqlParameter("@membership_expiry", userFarmerDTO.Farmer.MembershipExpiry),
                            new SqlParameter("@language_preference", userFarmerDTO.Farmer.LanguagePreference),
                            new SqlParameter("@farmer_inserted_by", userFarmerDTO.Farmer.InsertedBy),
                            new SqlParameter("@farmer_updated_by", userFarmerDTO.Farmer.UpdatedBy),
                            errorMessageParam
                        );

                        var errorMessage = errorMessageParam.Value != DBNull.Value ? (string)errorMessageParam.Value : null;
                        if (!string.IsNullOrEmpty(errorMessage))
                        {
                            errorMessages.Add(errorMessage);
                        }
                    }

                    if (errorMessages.Any())
                    {
                        await transaction.RollbackAsync();
                        return BadRequest(new { status = 500, success = false, messages = errorMessages });
                    }

                    await transaction.CommitAsync();
                    return Ok(new { status = 200, success = true, message = "Users and farmers created successfully." });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, new { success = false, message = "An error occurred while creating the users and farmers.", error = ex.Message });
                }
            }
        }


        [Authorize(Policy = "AdminFarmer")]
        [HttpGet("get_user/{userId}")]
 
        public async Task<ActionResult> GetUser(string userId)
        {
            try
            {
                var users = await _context.TbUsers
                    .FromSqlRaw("EXEC sp_GetUserById @user_id", new SqlParameter("@user_id", userId))
                    .AsNoTracking()
                    .ToListAsync();

                var user = users.FirstOrDefault();

                if (user == null)
                {
                    return NotFound(new { success = false, message = "User not found." });
                }

                return Ok(new { success = true, data = user });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while retrieving the user data.", error = ex.Message });
            }
        }

        [HttpGet("get_farmer/{farmerId}")]
        public async Task<ActionResult> GetFarmer(string farmerId)
        {
            try
            {
                Console.WriteLine("..."+ farmerId);
                var famers = await _context.TbFarmers
                    .FromSqlRaw("EXEC sp_GetFarmerById @farmer_id", new SqlParameter("@farmer_id", farmerId))
                    .AsNoTracking()
                    .ToListAsync();
                var farmer = famers.FirstOrDefault();

                if (farmer == null)
                {
                    return NotFound(new { success = false, message = "Farmer not found." });
                }
                return Ok(new { success = true, data = farmer });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while retrieving the famrmer data.", error = ex.Message });
            }
        }

        [HttpPut("update_user")]
        public async Task<IActionResult> UpdateUser(TbUser updatedUser)
        {
            if (string.IsNullOrEmpty(updatedUser.UserId))
            {
                return BadRequest(new { success = false, message = "User ID is required" });
            }

            if (string.IsNullOrEmpty(updatedUser.UpdatedBy))
            {
                return BadRequest(new { success = false, message = "Updated By information is required" });
            }

            try
            {
                var parameters = new List<SqlParameter>
            {
                new SqlParameter("@user_id", updatedUser.UserId),
                new SqlParameter("@user_updated_by", updatedUser.UpdatedBy)
            };

                if (!string.IsNullOrEmpty(updatedUser.UserName))
                    parameters.Add(new SqlParameter("@user_name", updatedUser.UserName));
                else
                    parameters.Add(new SqlParameter("@user_name", DBNull.Value));

                if (!string.IsNullOrEmpty(updatedUser.AadharNumber))
                    parameters.Add(new SqlParameter("@aadhar_number", updatedUser.AadharNumber));
                else
                    parameters.Add(new SqlParameter("@aadhar_number", DBNull.Value));

                if (!string.IsNullOrEmpty(updatedUser.Email))
                    parameters.Add(new SqlParameter("@user_email", updatedUser.Email));
                else
                    parameters.Add(new SqlParameter("@user_email", DBNull.Value));

                if (!string.IsNullOrEmpty(updatedUser.PhoneNumber))
                    parameters.Add(new SqlParameter("@phone_number", updatedUser.PhoneNumber));
                else
                    parameters.Add(new SqlParameter("@phone_number", DBNull.Value));

                if (!string.IsNullOrEmpty(updatedUser.Address))
                    parameters.Add(new SqlParameter("@user_address", updatedUser.Address));
                else
                    parameters.Add(new SqlParameter("@user_address", DBNull.Value));

                if (!string.IsNullOrEmpty(updatedUser.Password))
                    parameters.Add(new SqlParameter("@user_password", updatedUser.Password));
                else
                    parameters.Add(new SqlParameter("@user_password", DBNull.Value));

                if (!string.IsNullOrEmpty(updatedUser.RoleId))
                    parameters.Add(new SqlParameter("@role_id", updatedUser.RoleId));
                else
                    parameters.Add(new SqlParameter("@role_id", DBNull.Value));

                var errorMessageParam = new SqlParameter("@error_message", SqlDbType.NVarChar, 1000)
                {
                    Direction = ParameterDirection.Output
                };
                parameters.Add(errorMessageParam);

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_SingleUpdateUser @user_id, @user_name, @aadhar_number, @user_email, @phone_number, @user_address, @user_password, @role_id, @user_updated_by, @error_message OUTPUT",
                    parameters.ToArray()
                );

                var errorMessage = errorMessageParam.Value != DBNull.Value ? (string)errorMessageParam.Value : null;
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    return BadRequest(new { status = 500, success = false, message = errorMessage });
                }

                return Ok(new { status = 200, success = true, message = "User updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while updating the user.", error = ex.Message });
            }
        }

        [HttpPut("update_farmer")]
        public async Task<IActionResult> UpdateFarmer(TbFarmer updatedFarmer)
        {
            if (string.IsNullOrEmpty(updatedFarmer.FarmerId))
            {
                return BadRequest(new { success = false, message = "Farmer ID is required" });
            }

            if (string.IsNullOrEmpty(updatedFarmer.UpdatedBy))
            {
                return BadRequest(new { success = false, message = "Updated By information is required" });
            }

            try
            {
                var parameters = new List<SqlParameter>
            {
                new SqlParameter("@farmer_id", updatedFarmer.FarmerId),
                new SqlParameter("@farmer_updated_by", updatedFarmer.UpdatedBy)
            };

                if (!string.IsNullOrEmpty(updatedFarmer.UserId))
                    parameters.Add(new SqlParameter("@user_id", updatedFarmer.UserId));
                else
                    parameters.Add(new SqlParameter("@user_id", DBNull.Value));

                if (!string.IsNullOrEmpty(updatedFarmer.FarmSize))
                    parameters.Add(new SqlParameter("@farm_size", updatedFarmer.FarmSize));
                else
                    parameters.Add(new SqlParameter("@farm_size", DBNull.Value));

                if (!string.IsNullOrEmpty(updatedFarmer.FarmLocation))
                    parameters.Add(new SqlParameter("@farm_location", updatedFarmer.FarmLocation));
                else
                    parameters.Add(new SqlParameter("@farm_location", DBNull.Value));

                if (!string.IsNullOrEmpty(updatedFarmer.PinCode))
                    parameters.Add(new SqlParameter("@pin_code", updatedFarmer.PinCode));
                else
                    parameters.Add(new SqlParameter("@pin_code", DBNull.Value));

                if (!string.IsNullOrEmpty(updatedFarmer.IrrigationMethod))
                    parameters.Add(new SqlParameter("@irrigation_method", updatedFarmer.IrrigationMethod));
                else
                    parameters.Add(new SqlParameter("@irrigation_method", DBNull.Value));

                if (!string.IsNullOrEmpty(updatedFarmer.SoilType))
                    parameters.Add(new SqlParameter("@soil_type", updatedFarmer.SoilType));
                else
                    parameters.Add(new SqlParameter("@soil_type", DBNull.Value));

                if (updatedFarmer.FarmingExperience.HasValue)
                    parameters.Add(new SqlParameter("@farming_experience", updatedFarmer.FarmingExperience.Value));
                else
                    parameters.Add(new SqlParameter("@farming_experience", DBNull.Value));

                if (!string.IsNullOrEmpty(updatedFarmer.MembershipStatus))
                    parameters.Add(new SqlParameter("@membership_status", updatedFarmer.MembershipStatus));
                else
                    parameters.Add(new SqlParameter("@membership_status", DBNull.Value));

                if (updatedFarmer.MembershipExpiry.HasValue)
                    parameters.Add(new SqlParameter("@membership_expiry", updatedFarmer.MembershipExpiry.Value));
                else
                    parameters.Add(new SqlParameter("@membership_expiry", DBNull.Value));

                if (!string.IsNullOrEmpty(updatedFarmer.LanguagePreference))
                    parameters.Add(new SqlParameter("@language_preference", updatedFarmer.LanguagePreference));
                else
                    parameters.Add(new SqlParameter("@language_preference", DBNull.Value));

                var errorMessageParam = new SqlParameter("@update_error_message", SqlDbType.NVarChar, 1000)
                {
                    Direction = ParameterDirection.Output
                };
                parameters.Add(errorMessageParam);

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_SingleUpdateFarmer @farmer_id, @user_id, @farm_size, @farm_location, @pin_code, @irrigation_method, @soil_type, @farming_experience, @membership_status, @membership_expiry, @language_preference, @farmer_updated_by, @update_error_message OUTPUT",
                    parameters.ToArray()
                );

                var errorMessage = errorMessageParam.Value != DBNull.Value ? (string)errorMessageParam.Value : null;
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    return BadRequest(new { status = 500, success = false, message = errorMessage });
                }

                return Ok(new { status = 200, success = true, message = "Farmer updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while updating the farmer.", error = ex.Message });
            }
        }



    }
}

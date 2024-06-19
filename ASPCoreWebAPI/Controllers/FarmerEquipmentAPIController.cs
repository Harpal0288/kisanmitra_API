using ASPCoreWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ASPCoreWebAPI.Controllers
{
	[Authorize(Policy = "FarmerOrAdmin")]
	[Route("v1/api/kisan_mitra/farmer_equipment")]
	[ApiController]
	public class FarmerEquipmentAPIController : ControllerBase
	{
		private readonly ILogger<FarmerEquipmentAPIController> logger;
		private readonly KisanmitraKisanContext dbContext;

		public FarmerEquipmentAPIController(ILogger<FarmerEquipmentAPIController> logger,KisanmitraKisanContext dbContext)
		{
			this.dbContext = dbContext;
			this.logger = logger;
		}

		[HttpGet]
		[Route("get_all_farmer_equipment")]
		public async Task<ActionResult<List<TbFarmerEquipment>>> getAllFarmerEquipment(int page = 1, int pageSize = 10)
		{
			try
			{
				if (dbContext.TbFarmerEquipments.IsNullOrEmpty())
				{
					return NotFound("The resource TbFarmerEquipment is not available.");
				}

				var pageNumberParam = new SqlParameter("@PageNumber", page);
				var pageSizeParam = new SqlParameter("@PageSize", pageSize);

				var data = await dbContext.TbFarmerEquipments.FromSqlRaw("EXEC sp_GetAllFarmerEquipment @PageNumber, @PageSize", pageNumberParam, pageSizeParam).ToListAsync();
				if (data.IsNullOrEmpty())
				{
					return NotFound("No matching data found.");
				}
				return Ok(data);
			}
			catch (Exception e)
			{
				return Problem(detail: e.Message);
			}
		}

		[HttpGet]
		[Route("get_farmer_equipment_by_farmer_id/{f_id}")]
		public async Task<ActionResult<List<TbFarmerEquipment>>> getFarmerEquipmentById(string f_id)
		{
			try
			{
				if (f_id.IsNullOrEmpty())
				{
					return BadRequest("Farmer Id is required.");
				}

				if (dbContext.TbFarmerEquipments.IsNullOrEmpty())
				{
					return NotFound("The resource TbFarmerEquipment is not available.");
				}

				var farmerId = new SqlParameter("farmerId", f_id);
				var data = await dbContext.TbFarmerEquipments.FromSqlRaw("EXEC sp_GetFarmerEquipmentById @farmerId", farmerId).ToListAsync();

				if (data.IsNullOrEmpty())
				{
					return NotFound("No equipment found for the specified farmer ID.");
				}

				return Ok(data);
			}
			catch (InvalidOperationException)
			{
				return NotFound("No equipment found for the specified farmer ID.");
			}
			catch (Exception e)
			{
				return Problem(detail: e.Message);
			}
		}

		[HttpGet]
		[Route("get_single_farmer_equipment_by_id/{f_id}&{e_id}")]
		public async Task<ActionResult<TbFarmerEquipment>> getSingleFarmerEquipmentById(string f_id, string e_id)
		{
			try
			{
				if (f_id.IsNullOrEmpty())
				{
					return BadRequest("Farmer ID is required.");
				}

				if (e_id.IsNullOrEmpty())
				{
					return BadRequest("Equipment ID is required.");
				}

				if (dbContext.TbFarmerEquipments.IsNullOrEmpty())
				{
					return NotFound("The resource TbFarmerEquipment is not available.");
				}

				var farmerId = new SqlParameter("farmerId", f_id);
				var equipmentId = new SqlParameter("equipmentId", e_id);
				var data = await dbContext.TbFarmerEquipments.FromSqlRaw("EXEC sp_GetSingleFarmerEquipmentById @farmerId, @equipmentId", farmerId, equipmentId).ToListAsync();

				if (data.IsNullOrEmpty())
				{
					return NotFound("No equipment found for the specified farmer ID and equipment ID.");
				}

				return Ok(data);
			}
			catch (InvalidOperationException)
			{
				return NotFound("No equipment found for the specified farmer ID and equipment ID.");
			}
			catch (Exception e)
			{
				return Problem(detail: e.Message);
			}
		}

		[HttpPost]
		[Route("insert_farmer_equipment")]
		public async Task<ActionResult<TbFarmerEquipment>> insertFarmerEquipment(TbFarmerEquipment farmerEquipment)
		{
			try
			{
				if (farmerEquipment == null)
				{
					return BadRequest("Farmer equipment data is required.");
				}

				if (dbContext.TbFarmerEquipments.IsNullOrEmpty())
				{
					return NotFound("The resource TbFarmerEquipments is not available.");
				}

				var equipmentId = new SqlParameter("@equipmentId", farmerEquipment.EquipmentId);
				var farmerId = new SqlParameter("@farmerId", farmerEquipment.FarmerId);
				var insertedBy = new SqlParameter("@insertedBy", farmerEquipment.InsertedBy);
				var updatedBy = new SqlParameter("@updatedBy", farmerEquipment.UpdatedBy);

				var data = await dbContext.TbFarmerEquipments.FromSqlRaw("EXEC sp_SingleInsertFarmerEquipment @equipmentId, @farmerId, @insertedBy, @updatedBy", equipmentId, farmerId, insertedBy, updatedBy).ToListAsync();

				if (data.IsNullOrEmpty())
				{
					return NotFound("No matching record found to insert.");
				}

				return CreatedAtAction(nameof(insertFarmerEquipment), new { f_Id = data.First().FarmerId, e_id = data.First().EquipmentId }, data.First());
			}
			catch(InvalidOperationException ex)
			{
				logger.LogInformation(ex.Message);
				return Conflict("Data already exists.");
			}
			catch (Exception e)
			{
				return Problem(detail: e.Message);
			}
		}

		[HttpPut("update_farmer_equipment/{f_id}&{e_id}")]
		public async Task<ActionResult<TbFarmerEquipment>> updateFarmerEquipment(string f_id, string e_id, TbFarmerEquipment farmerEquipment)
		{
			try
			{
				if (f_id.IsNullOrEmpty())
				{
					return BadRequest("Farmer ID is required.");
				}

				if (e_id.IsNullOrEmpty())
				{
					return BadRequest("Equipment ID is required.");
				}

				if (farmerEquipment == null)
				{
					return BadRequest("Farmer equipment data is required.");
				}

				if (dbContext.TbFarmerEquipments.IsNullOrEmpty())
				{
					return NotFound("The resource TbFarmerEquipments is not available.");
				}

				if (f_id != farmerEquipment.FarmerId && e_id != farmerEquipment.EquipmentId)
				{
					return BadRequest("Mismatched farmer ID or equipment ID.");
				}

				var equipmentId = new SqlParameter("equipmentId", e_id);
				var farmerId = new SqlParameter("farmerId", f_id);
				var eq_id = new SqlParameter("newData", farmerEquipment.EquipmentId);
				var updatedBy = new SqlParameter("updatedBy", farmerEquipment.UpdatedBy);

				var data = await dbContext.TbFarmerEquipments.FromSqlRaw("EXEC sp_SingleUpdateFarmerEquipment @equipmentId, @farmerId, @updatedBy, @newData", equipmentId, farmerId, updatedBy, eq_id).ToListAsync();

				if (data.IsNullOrEmpty())
				{
					return NotFound("No matching record found to update.");
				}

				return Ok(data);
			}
			catch (Exception e)
			{
				return Problem(detail: e.Message);
			}
		}

		[HttpDelete("delete_farmer_equipment/{f_id}&{e_id}")]
		public async Task<ActionResult<TbFarmerEquipment>> deleteFarmerEquipment(string f_id, string e_id)
		{
			try
			{
				if (f_id.IsNullOrEmpty())
				{
					return BadRequest("Farmer ID is required.");
				}

				if (e_id.IsNullOrEmpty())
				{
					return BadRequest("Equipment ID is required.");
				}

				if (dbContext.TbFarmerEquipments.IsNullOrEmpty())
				{
					return NotFound("The resource TbFarmerEquipments is not available.");
				}

				var equipmentId = new SqlParameter("equipmentId", e_id);
				var farmerId = new SqlParameter("farmerId", f_id);

				var data = await dbContext.Database.ExecuteSqlRawAsync("EXEC sp_SingleDeleteFarmerEquipment @equipmentId, @farmerId", equipmentId, farmerId);

				if (data == -1)
				{
					return NotFound("No matching record found to delete.");
				}

				return NoContent();
			}
			catch (Exception e)
			{
				return Problem(detail: e.Message);
			}
		}
	}
}

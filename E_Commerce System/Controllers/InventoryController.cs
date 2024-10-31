using Application.BLL.Contracts;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Dtos.InventoryDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryServices _inventoryServices;
        public InventoryController(IInventoryServices inventoryServices)
        {
            _inventoryServices = inventoryServices;
        }

        [HttpGet("GetInventory")]
        public async Task<IActionResult> GetAsync()
        {
            var data = await _inventoryServices.GetInventoryIncludeAsync();
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<List<InventoryDataDto>>().Error(data);
        }
        [HttpGet("GetInventoryById")]
        public async Task<IActionResult> GetByIdAsync([FromQuery] Guid InventoryId)
        {
            var data = await _inventoryServices.GetInventoryByIdAsync(InventoryId);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<InventoryDataDto>().Error(data);
        }
        [HttpPut("UpdateInventory")]
        public async Task<IActionResult> UpdateInventoryAsync([FromQuery] Guid InventoryId)
        {
            var data = await _inventoryServices.UpdateInventoryAsync(InventoryId);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<string>().Error(data);
        }
        [HttpPut("DeleteInventory")]
        public async Task<IActionResult> DeleteInventoryAsync([FromQuery] Guid InventoryId)
        {
            var data = await _inventoryServices.DeleteInventoryAsync(InventoryId);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<string>().Error(data);
        }
    }
}

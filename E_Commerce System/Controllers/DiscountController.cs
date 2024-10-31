using Application.BLL.Contracts;
using Application.DAL.Domain.Models;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Dtos.DiscountDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountServices _discountservices;
        public DiscountController(IDiscountServices discountServices)
        {
            _discountservices = discountServices;
        }

        [HttpGet("ListDiscount")]
        public async Task<IActionResult> ListDiscount()
        {
            var data = await _discountservices.GetAsync();
            if(data.success)
            {
                return Ok(data.response);
            }
            return new ErrorResponse<List<DiscountDataDto>>().Error(data);
        }

        [HttpGet]
        [Route("ListDiscount/id")]
        public async Task<IActionResult> GetByIdAsync([FromQuery] Guid id)
        {
            var data = await _discountservices.GetByIdAsync(id);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<DiscountDataDto>().Error(data);
        }

        [HttpPost("CreateDiscount")]
        public async Task<IActionResult> CreateDiscount([FromBody] DiscountCreateDto discount)
        {
            var data = await _discountservices.CreateDiscountAsync(discount);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<string>().Error(data);
        }

        [HttpPost("AddDiscountFroProducts")]
        public async Task<IActionResult> AddDiscountFroProductsAsync([FromQuery] Guid idProduct, [FromQuery] Guid idDiscount)
        {
            var data = await _discountservices.AddDiscountFroProductsAsync(idProduct, idDiscount);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<string>().Error(data);
        }

        [HttpPut("UpdateDiscount")]
        public async Task<IActionResult> UpdateDiscountAsync([FromQuery] Guid id, [FromBody] DiscountCreateDto discountCreateDto)
        {
            var data = await _discountservices.UpdateDiscount(id, discountCreateDto);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<string>().Error(data);
        }

        [HttpDelete("DeleteDiscount")]
        public async Task<IActionResult> DeleteDiscount([FromQuery] Guid id)
        {
            var data = await _discountservices.DeleteDiscount(id);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<string>().Error(data);
        }
    }
}

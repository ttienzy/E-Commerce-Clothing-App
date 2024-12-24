using Application.BLL.Contracts;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Dtos.CartDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCart([FromQuery] Guid userId)
        {
            var data = await _cartService.GetAllCartByUserId(userId);
            if (data.success)
            {
                return Ok(data.response);
            }
            return new ErrorResponse<List<CartDataDto>>().Error(data);
        }
        [HttpGet("GetCount")]
        public IActionResult GetCount([FromQuery]Guid id)
        {
            return Ok(_cartService.GetCountCart(id));
        }
        [HttpPost]
        public async Task<IActionResult> AddCart([FromBody] AddCartDto addCartDto)
        {
            var data = await _cartService.AddCartDto(addCartDto);
            if (data.success)
            {
                return Ok(data.response);
            }
            return new ErrorResponse<string>().Error(data);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCart([FromQuery] Guid cartId)
        {
            var data = await _cartService.DeleteCart(cartId);
            if (data.success) 
                return Ok(data.response);
            return new ErrorResponse<string>().Error(data);
        }
    }
}

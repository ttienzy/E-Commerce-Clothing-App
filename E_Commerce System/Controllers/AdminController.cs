using Application.BLL.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public readonly IPageManagers _pageManagers;
        public readonly IPaymentServices _paymentServices;
        public AdminController(IPageManagers pageManagers, IPaymentServices paymentServices)
        {
            _pageManagers = pageManagers;
            _paymentServices = paymentServices; 
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _pageManagers.InfoForManager();
            if (data.success)
            {
                return Ok(data.response);
            }
            return BadRequest();    
        }
        [HttpGet("GetBestSelling")]
        public async Task<IActionResult> GetBestSelling()
        {
            var data = await _pageManagers.SoftByBestSellingProduct();
            if (data.success)
            {
                return Ok(data.response);
            }
            return BadRequest();
        }
        [HttpGet("CheckOrderStatus")]
        public async Task<IActionResult> CheckOrderStatus()
        {
            var data = await _paymentServices.CheckOrderStatus();
            if (data.success)
            {
                return Ok(data.response);
            }
            return BadRequest();
        }
        [HttpPut("RemoveOrder")]
        public async Task<IActionResult> RemoveOrder([FromQuery]Guid idProduct, [FromQuery] string orderInfo)
        {
            var data = await _paymentServices.RemoveOrder(idProduct, orderInfo);
            if (data.success)
            {
                return Ok(data.response);
            }
            return BadRequest(data);
        }

        [Authorize(Roles = "User")]
        [HttpGet("demo-1")]
        public IActionResult Demo01()
        {
            return Ok(HttpContext.User);
        }
    }
}

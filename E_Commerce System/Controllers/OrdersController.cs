using Application.BLL.Contracts;
using Application.DAL.Domain.Models;
using Application.DAL.Shared;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Dtos.OrderDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderInvoiceServices _orderInvoiceServices;
        public OrdersController(IOrderInvoiceServices orderInvoiceServices)
        {
            _orderInvoiceServices = orderInvoiceServices;
        }

        [HttpGet("OrderHistory")]
        public IActionResult OrderHistory([FromQuery] Guid client_id,int pageNumber = 1,int pageSize = 2)
        {
            var data = _orderInvoiceServices.ListOrderHistoryAsync(pageNumber, pageSize,client_id);
            if (data.success)
            {
                return Ok(data.response);
            }
            return new ErrorResponse<PagedList<OrderHistoryDto>>().Error(data);
        }
        [HttpGet("OrderPreview")]
        public async Task<IActionResult> ListOrderPreviewAsync([FromQuery] Guid client_id)
        {
            var data = await _orderInvoiceServices.ListOrderPreviewAsync(client_id);
            if (data.success)
            {
                return Ok(data.response);
            }
            return new ErrorResponse<List<OrderHistoryDto>>().Error(data);
        }

        [HttpGet("RevenueInfo")]
        public async Task<IActionResult> RevenueInfo()
        {
            var data = await _orderInvoiceServices.RevenueInfo();
            if (data.success)
            {
                return Ok(data.response);
            }
            return new ErrorResponse<List<RevenueDto>>().Error(data);
        }

        [HttpPut("OrderReview")]
        public async Task<IActionResult> OrderReview([FromQuery] Guid order_id,string des)
        {
            var data = await _orderInvoiceServices.AddReviewAsync(order_id, des);
            if (data.success)
            {
                return Ok(data.response);   
            }
            return new ErrorResponse<Orders>().Error(data);
        }
    }
}

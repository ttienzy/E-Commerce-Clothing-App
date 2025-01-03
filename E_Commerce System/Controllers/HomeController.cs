﻿using Application.BLL.Contracts;
using Application.BLL.PaymentServices.PayContracts;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.PaymentModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly IPaymentServices _paymentServices;
        public HomeController(IVnPayService vnPayService, IPaymentServices paymentServices)
        {
            _vnPayService = vnPayService;
            _paymentServices = paymentServices;
        }

        [HttpGet("PaymentCallback")]
        public async Task<IActionResult> PaymentCallback()
        {
            var result = await _vnPayService.PaymentExecute(Request.Query);
            if(result.success)
            {
                var getresponse = result.response;
                if (getresponse.Success)
                {
                    return Redirect($"http://localhost:3000/payment_callback?order_id={getresponse.OrderId}&amount={getresponse.Amount}&order_info={getresponse.OrderDescription}");
                }
                return Redirect("http://localhost:3000/user");
            }
            return new ErrorResponse<PaymentResponseModel>().Error(result);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreatePaymentUrl([FromBody]ImfomationOrderCreate model)
        {
            var result = await _vnPayService.CreatePaymentUrl(model, HttpContext);

            if(result.success)
                return Ok(new { url = result.response });
            return new ErrorResponse<string>().Error(result);
        }

        [HttpPut("CacelledPayment")]
        public async Task<IActionResult> CacelledPayment([FromQuery] Guid IdPayment)
        {
            var data = await _paymentServices.CaceledPayment(IdPayment);
            if(data.success)
                return Ok(data.response);
            return new ErrorResponse<string>().Error(data);
        }
    }
}

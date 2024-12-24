using Application.BLL.Contracts;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Common;
using Application.DAL.Shared.Dtos.InfoProviderDto;
using Application.DAL.Shared.Dtos.ProductDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly ITradingProductSupplierInfo _productSupplierInfo;
        public ProviderController(ITradingProductSupplierInfo tradingProductSupplierInfo)
        {
            _productSupplierInfo = tradingProductSupplierInfo;
        }
        [HttpGet("Provider")]
        public async Task<IActionResult> ListProviderAsync()
        {
            var result = await _productSupplierInfo.ListProviderAsync();
            if (result.success)
            {
                return Ok(result.response);
            }
            return new ErrorResponse<List<ProviderDataDto>>().Error(result);
        }

        [HttpGet("Provider/searchbyname")]
        public async Task<IActionResult> SearchProviderByNameAsync([FromQuery] string NameProducts)
        {

            var result = await _productSupplierInfo.SearchProviderByNameAsync(NameProducts);
            if (result.success)
            {
                return Ok(result.response);
            }
            return new ErrorResponse<List<ProviderDataDto>>().Error(result);
        }

        [HttpGet("Provider/searchbytelno")]
        public async Task<IActionResult> FindProviderAsync([FromQuery] string PhoneNumber)
        {   
            var data = await _productSupplierInfo.FindProviderAsync(PhoneNumber);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<ProviderDataDto>().Error(data);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateProviderAsync([FromBody] ProviderDataDto providerDataDto)
        {
            var data = await _productSupplierInfo.CreateProviderAsync(providerDataDto);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<string>().Error(data);
        }
        [HttpPost("TransactionProvider")]
        public async Task<IActionResult> TransactionProvider([FromForm] InfoTransactionProviderDto infoTransactionProviderDto)
        {
            var results = await _productSupplierInfo.TransactionProviderAsync(infoTransactionProviderDto);
            if (results.success)
            {
                return Ok(results.response);
            }
            return new ErrorResponse<InfoTransactionProviderDto>().Error(results);
        }
        [HttpGet("GetCountUnPaid")]
        public async Task<IActionResult> PaymentUnPaidById([FromQuery] Guid idUser)
        {
            var data = await _productSupplierInfo.PaymentUnPaidById(idUser);
            if (data.success)
            {
                return Ok(data.response);
            }
            return new ErrorResponse<int>().Error(data);
        }
        [HttpPost("PurchaseProduct")]
        public async Task<IActionResult> PurchaseProduct([FromBody] ProductShipDto productShipDto)
        {
            var data = await _productSupplierInfo.TransactionClient(productShipDto);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<string>().Error(data);
        }
        [HttpPut("CanceledProduct")]
        public async Task<IActionResult> CanceledProduct([FromQuery] Guid idPayment)
        {
            var data = await _productSupplierInfo.CancelOrder(idPayment);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<string>().Error(data);
        }
        [HttpPut("Provider/update")]
        public async Task<IActionResult> UpdateProviderAsync([FromQuery] string PhoneNumber)
        {
            var data = await _productSupplierInfo.UpdateProviderAsync(PhoneNumber);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<string>().Error(data);
        }
    }
}

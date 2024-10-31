using Application.BLL.Contracts;
using Application.DAL.Domain.Models;
using Application.DAL.Domain.QuerryParams;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Dtos.ProductDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productservices;
        public ProductController(IProductServices productServices)
        {
            _productservices = productServices;
        }

        [HttpGet("ListProduct")]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _productservices.GetAsync();
            if(data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<List<ProductData>>().Error(data);
        }
        [HttpGet("ListProduct/{skip}/{take}")]
        public async Task<IActionResult> GetAsync( int skip, int take)
        {
            var data = await _productservices.GetProductsAsync(skip, take);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<List<ProductData>>().Error(data);
        }
        [HttpGet("ListProductById")]
        public async Task<IActionResult> GetAllAsync([FromQuery] Guid ProductId)
        {
            var data = await _productservices.GetByIdAsync(ProductId);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<ProductData>().Error(data);
        }
        [HttpGet("ListProductInclude")]
        public async Task<IActionResult> GetInclude([FromQuery] ProductQuerryParams productQuerryParams)
        {
            var data = await _productservices.GetProductIncludedDiscount(productQuerryParams);
            if (data.success)
            {
                return Ok(data.response);
            }
            return new ErrorResponse<List<ProductIncludedDiscountDto>>().Error(data);
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromQuery] Guid id)
        {
            var data = await _productservices.DeleteProduct(id);    
            if (data.success)
            {
                return Ok(data);    
            }
            return new ErrorResponse<Products>().Error(data);
        }
    }
}

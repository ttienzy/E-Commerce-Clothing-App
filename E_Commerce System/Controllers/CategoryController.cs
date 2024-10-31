using Application.BLL.Contracts;
using Application.BLL.Services;
using Application.DAL.Domain.Models;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Dtos.CategoryDto;
using Application.DAL.Shared.Dtos.DiscountDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet("ListCategory")]
        public async Task<IActionResult> ListCategory()
        {
            var data = await _categoryServices.GetAsync();
            if (data.success)
            {
                return Ok(data.response);
            }
            return new ErrorResponse<List<CategoryDto>>().Error(data);
        }
        [HttpGet("Category/Id")]
        public async Task<IActionResult> GetByIdAsync([FromQuery] Guid CategoryId)
        {
            var data = await _categoryServices.GetByIdAsync(CategoryId);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<CategoryDto>().Error(data);
        }
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryCreateDto categoryCreateDto)
        {
            var data = await _categoryServices.CreateCategoryAsync(categoryCreateDto);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<string>().Error(data);
        }
        [HttpPost("AddCategoryForProduct")]
        public async Task<IActionResult> AddRelationCategoryFroProductsAsync([FromQuery]Guid ProductId, [FromQuery] Guid categoryId)
        {
            var data = await _categoryServices.AddRelationCategoryFroProductsAsync(ProductId, categoryId);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<string>().Error(data);
        }
        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCaregory([FromQuery] Guid categoryId, [FromBody]CategoryCreateDto categoryCreateDto)
        {
            var data = await _categoryServices.UpdateCaregory(categoryId, categoryCreateDto);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<string>().Error(data);
        }
        [HttpDelete("RequirementCategory")]
        public async Task<IActionResult> DeleteCategory([FromQuery] Guid productId)
        {
            var data = await _categoryServices.DeleteCategory(productId);
            if (data.success)
            {
                return Ok(data);
            }
            return new ErrorResponse<string>().Error(data);
        }
    }
}

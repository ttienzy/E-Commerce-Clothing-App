using Application.DAL.Shared.Base;
using Application.DAL.Shared.Dtos.CategoryDto;
using Application.DAL.Shared.Dtos.DiscountDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Contracts
{
    public interface ICategoryServices
    {
        Task<BaseResponse<List<CategoryDto>>> GetAsync();
        Task<BaseResponse<CategoryDto>> GetByIdAsync(Guid id);
        Task<BaseResponse<string>> CreateCategoryAsync(CategoryCreateDto categoryCreateDto);
        Task<BaseResponse<string>> AddRelationCategoryFroProductsAsync(Guid ProductId, Guid categoryId);
        Task<BaseResponse<string>> UpdateCaregory(Guid categoryId, CategoryCreateDto categoryCreateDto);
        Task<BaseResponse<string>> DeleteCategory(Guid productId);
    }
}

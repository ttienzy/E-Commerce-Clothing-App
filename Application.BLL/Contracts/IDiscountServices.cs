using Application.DAL.Domain.Models;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Dtos.DiscountDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Contracts
{
    public interface IDiscountServices
    {
        Task<BaseResponse<List<DiscountDataDto>>> GetAsync();
        Task<BaseResponse<DiscountDataDto>> GetByIdAsync(Guid id);
        Task<BaseResponse<string>> CreateDiscountAsync(DiscountCreateDto discountCreateDto);
        Task<BaseResponse<string>> AddDiscountFroProductsAsync(Guid ProductId,Guid DiscountId);
        Task<BaseResponse<string>> UpdateDiscount(Guid ProductId, DiscountCreateDto discountCreateDto);
        Task<BaseResponse<string>> DeleteDiscount(Guid ProductId);
    }
}

using Application.DAL.Domain.Models;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Dtos.CartDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Contracts
{
    public interface ICartService
    {
        Task<BaseResponse<List<CartDataDto>>> GetAllCartByUserId(Guid userId);
        int GetCountCart(Guid id);
        Task<BaseResponse<string>> AddCartDto(AddCartDto addCartDto);
        Task<BaseResponse<string>> DeleteCart(Guid cartId);

    }
}

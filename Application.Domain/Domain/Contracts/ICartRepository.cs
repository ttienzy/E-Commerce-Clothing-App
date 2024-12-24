using Application.DAL.Domain.Models;
using Application.DAL.Shared.Dtos.CartDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Contracts
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<List<CartDataDto>> GetAllCartByUserId(Guid userId);
        //Task<string> AddCartDto(AddCartDto addCartDto);
    }
}

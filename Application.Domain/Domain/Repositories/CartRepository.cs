using Application.DAL.DbContextData;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using Application.DAL.Shared.Dtos.CartDTO;
using Application.DAL.Shared.Dtos.OrderDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Repositories
{
    public class CartRepository : GenericRepositoryBase<Cart>, ICartRepository
    {
        public CartRepository(dbContext context) : base(context)
        {
        }

        

        public async Task<List<CartDataDto>> GetAllCartByUserId(Guid userId)
        {
            var cartList = from c in _context.carts
                           join p in _context.products on c.ProductId equals p.Id
                           join d in _context.discounts on p.DiscountId equals d.Id into discountGroup
                           from d in discountGroup.DefaultIfEmpty()
                           where c.UserId == userId
                           select new CartDataDto
                           {
                               Id = c.Id,
                               ProductId = p.Id,
                               ImageProducts = p.ImageProducts,
                               Name = p.Name,
                               Quantity = p.Quantity,
                               Price = p.Price,
                               DiscountCode = d.Discount_percent,
                           };
            return await cartList.ToListAsync();
        }
    }
}

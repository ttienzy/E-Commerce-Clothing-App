using Application.DAL.DbContextData;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Repositories
{
    public class OrderItemsRepository : GenericRepositoryBase<OrderItems>, IOrderItemsRepository
    {
        public OrderItemsRepository(dbContext context) : base(context)
        {
        }

        public async Task<OrderItems?> FindByOrderId(Guid orderId)
        {
            return await _context.orderItems.FirstOrDefaultAsync(e => e.OrderId == orderId);
        }
    }
}

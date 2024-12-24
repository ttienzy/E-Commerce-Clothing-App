using Application.DAL.DbContextData;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using Application.DAL.Shared.Dtos.OrderDtos;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Repositories
{
    public class OrderPaymentsRepository : GenericRepositoryBase<OrderPayment>, IOrderPaymentsRepository
    {
        public OrderPaymentsRepository(dbContext context) : base(context)
        {
        }

        public async Task<int> GetCountUnPaidById(Guid idUser)
        {
            var data = await (from p in _context.orderPayment
                       join o in _context.orders on p.OrderId equals o.Id
                       where (p.OrderInfo == "unpaid" || p.OrderInfo == "confirmed" || p.OrderInfo == "paid") && (o.UserId == idUser)
                       select new { 
                            IdPayment = p.Id
                       }).ToListAsync();

            return data.Count;

        }
        public decimal SumRevenue()
        {
            var values = new[] { "received", "paid" };
            var sum = _context.orderPayment.Where(e => values.Contains(e.OrderInfo)).Sum(e => e.Amount);
            return sum;
        }
        public async Task<List<OrderHistoryDto>> CheckOrderStatus()
        {
            var query = from pm in _context.orderPayment
                        join os in _context.orders on pm.OrderId equals os.Id
                        join ots in _context.orderItems on os.Id equals ots.OrderId
                        join p in _context.products on ots.ProductId equals p.Id
                        where pm.OrderInfo != "received" && pm.OrderInfo != "cancelled"
                        select new OrderHistoryDto
                        {
                            PaymentId = pm.Id,
                            OrderId = pm.OrderId,
                            Status = pm.OrderInfo,
                            CreatedAt = (DateTime)pm.CreatedAt,
                            Quantity = ots.QuantityProductOrder,
                            TotalMoney = os.TotalOrderMoney,
                            ProductName = p.Name,
                            UrlImage = p.ImageProducts,
                        };
            return await query.ToListAsync();
        }
    }
}

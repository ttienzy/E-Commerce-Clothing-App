using Application.DAL.DbContextData;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using Application.DAL.Shared.Dtos.OrderDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Repositories
{
    public class OrdersRepository : GenericRepositoryBase<Orders>, IOrdersRepository
    {
        public OrdersRepository(dbContext context) : base(context)
        {
        }

        public IQueryable<OrderHistoryDto> ListOrderInclude(Guid client_id)
        {
            var query = from order in _context.orders
                        join payment in _context.orderPayment on order.Id equals payment.OrderId
                        join orderitem in _context.orderItems on order.Id equals orderitem.OrderId
                        join product in _context.products on orderitem.ProductId equals product.Id
                        where order.UserId == client_id 

                        select new OrderHistoryDto
                        {
                            OrderId = order.Id,
                            Quantity = orderitem.QuantityProductOrder,
                            TotalMoney = order.TotalOrderMoney,
                            CreatedAt = order.CreatedAt,
                            ProductName = product.Name,
                            UrlImage = product.ImageProducts,
                            Status = payment.OrderInfo
                        };
            return query.AsQueryable();
            
        }
        public IQueryable<OrderHistoryDto> ListOrderIncludeUnPaid(Guid client_id)
        {
            var query = from order in _context.orders
                        join payment in _context.orderPayment on order.Id equals payment.OrderId
                        join orderitem in _context.orderItems on order.Id equals orderitem.OrderId
                        join product in _context.products on orderitem.ProductId equals product.Id
                        where order.UserId == client_id && (payment.OrderInfo == "unpaid" || payment.OrderInfo == "paid" || payment.OrderInfo == "confirmed")

                        select new OrderHistoryDto
                        {
                            PaymentId = payment.Id,
                            OrderId = order.Id,
                            Quantity = orderitem.QuantityProductOrder,
                            TotalMoney = order.TotalOrderMoney,
                            CreatedAt = order.CreatedAt,
                            ProductName = product.Name,
                            UrlImage = product.ImageProducts,
                            Status = payment.OrderInfo
                        };
            return query.AsQueryable();

        }
        public async Task<List<OrderHistoryDto>> ListOrderIncludeAllStatus()
        {
            var query = from order in _context.orders
                        join payment in _context.orderPayment on order.Id equals payment.OrderId
                        join orderitem in _context.orderItems on order.Id equals orderitem.OrderId
                        join product in _context.products on orderitem.ProductId equals product.Id
                        where payment.OrderInfo != "received"
                        select new OrderHistoryDto
                        {
                            PaymentId = payment.Id,
                            OrderId = order.Id,
                            Quantity = orderitem.QuantityProductOrder,
                            TotalMoney = order.TotalOrderMoney,
                            CreatedAt = order.CreatedAt,
                            ProductName = product.Name,
                            UrlImage = product.ImageProducts,
                            Status = payment.OrderInfo
                        };
            return await query.ToListAsync();
        }
        public  Task<List<OrderHistoryDto>> ListOrderPreview(Guid client_id)
        {
            var query = from order in _context.orders
                        join payment in _context.orderPayment on order.Id equals payment.OrderId
                        join orderitem in _context.orderItems on order.Id equals orderitem.OrderId
                        join product in _context.products on orderitem.ProductId equals product.Id
                        where order.UserId == client_id && payment.OrderInfo == "received"

                        select new OrderHistoryDto
                        {
                            OrderId = order.Id,
                            Quantity = orderitem.QuantityProductOrder,
                            TotalMoney = order.TotalOrderMoney,
                            CreatedAt = order.CreatedAt,
                            ProductName = product.Name,
                            UrlImage = product.ImageProducts,
                            Status = payment.OrderInfo
                        };
            return query.ToListAsync();

        }
        public async Task<List<RevenueDto>> RevenueInfo()
        {
            var result = await _context.orders
                .GroupBy(order => new { Month = order.CreatedAt.Month })
                .Select(g => new RevenueDto
                {
                    Month = g.Key.Month,
                    Revenue = g.Sum(order => order.TotalOrderMoney),
                })
                .OrderBy(x => x.Month)
                .ToListAsync();
            return result;
        }
        
    }
}

using Application.DAL.DbContextData;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using Application.DAL.Shared.Dtos.ManagerDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Repositories
{
    public class ProvidersRepository : GenericRepositoryBase<Providers>, IProvidersRepository
    {
        public ProvidersRepository(dbContext context) : base(context)
        {
        }

        public async Task<Providers?> FindByTelNoAsync(string TelNo)
        {
            return await _context.providers.FirstOrDefaultAsync(e => e.TelNo.Equals(TelNo));
        }
        public async Task<ManagerInfoDto> InfoForManager()
        {
            var values = new[] { "received", "paid" };
            var values1 = new[] { "unpaid", "paid" };
            var metrics = new ManagerInfoDto
            {
                TotalRevenue = await _context.orderPayment.Where(e => values.Contains(e.OrderInfo)).SumAsync(e => e.Amount),
                TotalOrders = await _context.orderPayment.CountAsync(),
                TotalUsers = await _context.Users.CountAsync(),
                TotalCategories = await _context.categories.CountAsync(),
                TotalDiscounts = await _context.discounts.CountAsync(),
                TotalInventory = await _context.products.SumAsync(p => p.Quantity),
                OnlineOrders = await _context.orderPayment.CountAsync(e => e.ProviderPayment == "VnPay"),
                OfflineOrders = await _context.orderPayment.CountAsync(e => e.ProviderPayment == "Offline"),
                TotalOrderStatus = await _context.orderPayment.CountAsync(e => values1.Contains(e.OrderInfo))
            };
            return metrics;
        }
        public async Task<List<BestSellingProduct>> SoftByBestSellingProduct()
        {
            var query = @"
                SELECT TOP 6
                    p.Name,
                    COUNT(oi.OrderId) as OrderCount
                FROM products p
                JOIN orderItems oi ON p.Id = oi.ProductId
                JOIN orders os ON oi.OrderId = os.Id
                JOIN orderPayment pm ON os.Id = pm.OrderId
                WHERE pm.OrderInfo NOT IN('cancelled','unpaid')
                GROUP BY p.Name
                ORDER BY OrderCount DESC
            ";
            using var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = query;

            using var reader = await command.ExecuteReaderAsync();

            var result = new List<BestSellingProduct>();
            while (await reader.ReadAsync())
            {
                result.Add(new BestSellingProduct
                {
                    ProductName = reader.GetString(0),
                    OrderCount = reader.GetInt32(1)
                });
            }
            return result;
        }
    }
}

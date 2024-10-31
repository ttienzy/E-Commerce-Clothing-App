using Application.DAL.Domain.Models;
using Application.DAL.Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Contracts
{
    public interface IOrdersRepository : IGenericRepository<Orders>
    {
        IQueryable<OrderHistoryDto> ListOrderInclude(Guid client_id);
        Task<List<OrderHistoryDto>> ListOrderPreview(Guid client_id);
        Task<List<RevenueDto>> RevenueInfo();
    }
}

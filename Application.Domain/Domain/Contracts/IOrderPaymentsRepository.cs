using Application.DAL.Domain.Models;
using Application.DAL.Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Contracts
{
    public interface IOrderPaymentsRepository : IGenericRepository<OrderPayment>
    {
        Task<int> GetCountUnPaidById(Guid idUser);
        Task<List<OrderHistoryDto>> CheckOrderStatus();
    }
}

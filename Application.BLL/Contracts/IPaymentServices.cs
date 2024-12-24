using Application.DAL.Shared.Base;
using Application.DAL.Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Contracts
{
    public interface IPaymentServices
    {
        Task<BaseResponse<string>> CaceledPayment(Guid IdPayment);
        Task<BaseResponse<List<OrderHistoryDto>>> CheckOrderStatus();
        Task<BaseResponse<string>> RemoveOrder(Guid idPayment, string OrderInfo);
    }
}

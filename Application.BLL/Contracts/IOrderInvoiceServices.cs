using Application.DAL.Domain.Models;
using Application.DAL.Shared;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Dtos.OrderDtos;
using Application.DAL.Shared.PaymentModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Contracts
{
    public interface IOrderInvoiceServices
    {

        BaseResponse<PagedList<OrderHistoryDto>> ListOrderHistoryAsync(int pageNumber, int pageSize, Guid clien_id);
        Task<BaseResponse<List<OrderHistoryDto>>> ListOrderPreviewAsync(Guid clien_id);
        Task<BaseResponse<Orders>> AddReviewAsync(Guid orderId,string des);
        Task<BaseResponse<List<RevenueDto>>> RevenueInfo();
    }
}

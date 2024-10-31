using Application.BLL.Contracts;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using Application.DAL.Shared;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Common;
using Application.DAL.Shared.Dtos.OrderDtos;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.BLL.Services
{
    public class OrderInvoiceServices : IOrderInvoiceServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderInvoiceServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Orders>> AddReviewAsync(Guid orderId,string des)
        {
            try
            {
                var order = await _unitOfWork.tb_Orders.FindByIdAsync(orderId);
                if (order == null)
                {
                    return new BaseResponse<Orders>().NotFound("This order isn't exists");
                }
                order.Description = des;
                _unitOfWork.tb_Orders.Update(order);
                await _unitOfWork.SaveChangeAsync();
                return new BaseResponse<Orders>().Success(order);
            }
            catch (Exception ex)
            {
                return new BaseResponse<Orders>().InternalServerError(ex.Message);
            }
        }

        public BaseResponse<PagedList<OrderHistoryDto>> ListOrderHistoryAsync(int pageNumber, int pageSize,Guid client_id)
        {
            try
            {
                var template = _unitOfWork.tb_Orders.ListOrderInclude(client_id).OrderByDescending(x => x.CreatedAt);
                var results = PagedList<OrderHistoryDto>.ToPagedList(template, pageNumber, pageSize);
                return new BaseResponse<PagedList<OrderHistoryDto>>().Success(results);
            }
            catch (Exception ex)
            {
                return new BaseResponse<PagedList<OrderHistoryDto>>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<List<OrderHistoryDto>>> ListOrderPreviewAsync(Guid clien_id)
        {
            try
            {
                var data = await _unitOfWork.tb_Orders.ListOrderPreview(clien_id);
                return new BaseResponse<List<OrderHistoryDto>>().Success(data);
            }
            catch(Exception ex)
            {
                return new BaseResponse<List<OrderHistoryDto>>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<List<RevenueDto>>> RevenueInfo()
        {
            try
            {
                var data = await _unitOfWork.tb_Orders.RevenueInfo();
                if (data.Count == 0)
                {
                    return new BaseResponse<List<RevenueDto>>().NotFound(ErrorMessage.EMPTY_RECORD);
                }
                return new BaseResponse<List<RevenueDto>>().Success(data);
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<RevenueDto>>().InternalServerError(ex.Message);
            }
        }
    }
}

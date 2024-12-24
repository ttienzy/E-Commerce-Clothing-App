using Application.BLL.Contracts;
using Application.DAL.Domain.Contracts;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public PaymentServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<string>> CaceledPayment(Guid IdPayment)
        {
            try
            {
                var data = await _unitOfWork.tb_OrderPayments.FindByIdAsync(IdPayment);
                if(data is null)
                {
                    return new BaseResponse<string>().NotFound("Not found payment");
                }
                data.OrderInfo = "cancelled";
                _unitOfWork.tb_OrderPayments.Update(data);

                await _unitOfWork.SaveChangeAsync();
                return new BaseResponse<string>().Success("Success");
            }
            catch(Exception ex)
            {
                return new BaseResponse<string>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<List<OrderHistoryDto>>> CheckOrderStatus()
        {
            try
            {
                var data = await _unitOfWork.tb_OrderPayments.CheckOrderStatus();
                if (data.Count == 0 )
                {
                    return new BaseResponse<List<OrderHistoryDto>>().NotFound("Not found payment");
                }
                return new BaseResponse<List<OrderHistoryDto>>().Success(data);
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<OrderHistoryDto>>().InternalServerError(ex.Message);
            }
        }
        public async Task<BaseResponse<string>> RemoveOrder(Guid idPayment, string OrderInfo)
        {
            try
            {
                var xPayment = await _unitOfWork.tb_OrderPayments.FindByIdAsync(idPayment);
                if (xPayment is null)
                    return new BaseResponse<string>().NotFound("Not Found");
                xPayment.OrderInfo = OrderInfo;

                _unitOfWork.tb_OrderPayments.Update(xPayment);
                await _unitOfWork.SaveChangeAsync();
                return new BaseResponse<string>().Success("Success");
            }
            catch(Exception ex)
            {
                return new BaseResponse<string>().InternalServerError(ex.Message);
            }
        }
    }
}

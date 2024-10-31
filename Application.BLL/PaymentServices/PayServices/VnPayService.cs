using Application.BLL.PaymentServices.Libraries;
using Application.BLL.PaymentServices.PayContracts;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.PaymentModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.PaymentServices.PayServices
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memoryCache;
        public VnPayService(IConfiguration configuration, IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
        }
        public async Task<BaseResponse<string>> CreatePaymentUrl(ImfomationOrderCreate model, HttpContext context)
        {
            try
            {
                var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
                var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
                //var tick = DateTime.Now.Ticks.ToString();
                var pay = new VnPayLibrary();

                var temprary_Order = new Orders
                {
                    UserId = model.UserId,
                    TotalOrderMoney = model.Amount * model.Quantity,
                };
                var tempraty_OrderItems = new OrderItems
                {
                    OrderId = temprary_Order.Id,
                    ProductId = model.ProductId,
                    QuantityProductOrder = model.Quantity,
                    Price = model.Amount,
                };
                _memoryCache.Set("tempraty_OrderItems", tempraty_OrderItems, TimeSpan.FromMinutes(3));

                pay.AddRequestData("vnp_Version", _configuration["Vnpay:vnp_Version"]);
                pay.AddRequestData("vnp_Command", _configuration["Vnpay:vnp_Command"]);
                pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:vnp_TmnCode"]);
                pay.AddRequestData("vnp_Amount", (((int)model.Amount * 100) * model.Quantity).ToString());
                pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
                pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:vnp_CurrCode"]);
                pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
                pay.AddRequestData("vnp_Locale", _configuration["Vnpay:vnp_Locale"]);
                pay.AddRequestData("vnp_OrderInfo", $"Online Payment");
                pay.AddRequestData("vnp_OrderType", "DIG");
                pay.AddRequestData("vnp_ReturnUrl", _configuration["PaymentCallBack:vnp_ReturnUrl"]);
                pay.AddRequestData("vnp_ExpireDate", timeNow.AddMinutes(15).ToString("yyyyMMddHHmmss"));
                pay.AddRequestData("vnp_TxnRef", temprary_Order.Id.ToString());

                var paymentUrl =
                    pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);


                await _unitOfWork.tb_Orders.AddAsync(temprary_Order);
                await _unitOfWork.SaveChangeAsync();

                return new BaseResponse<string>().Success(paymentUrl);
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<PaymentResponseModel>> PaymentExecute(IQueryCollection collections)
        {
            try
            {
                var pay = new VnPayLibrary();
                var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);

                if (!response.Success)
                {
                    var order = await _unitOfWork.tb_Orders.FindByIdAsync(Guid.Parse(response.OrderId));

                    
                    _unitOfWork.tb_Orders.Delete(order);
                    await _unitOfWork.SaveChangeAsync();

                    return new BaseResponse<PaymentResponseModel>().Success(response);
                }

                var newPayment = new OrderPayment
                {
                    ProviderPayment = "VnPay",
                    Amount = decimal.Parse(response.Amount)/100,
                    CreatedAt = DateTime.Now,
                    OrderId = Guid.Parse(response.OrderId),
                    OrderInfo = "Đã thanh toán"
                };
                
                await _unitOfWork.tb_OrderPayments.AddAsync(newPayment);
                await _unitOfWork.tb_OrderItems.AddAsync((OrderItems)_memoryCache.Get("tempraty_OrderItems"));
                await _unitOfWork.SaveChangeAsync();

                return new BaseResponse<PaymentResponseModel>().Success(response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<PaymentResponseModel>().InternalServerError(ex.Message);
            }
        }
    }
}

using Application.DAL.Shared.Base;
using Application.DAL.Shared.PaymentModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.PaymentServices.PayContracts
{
    public interface IVnPayService
    {
        Task<BaseResponse<string>> CreatePaymentUrl(ImfomationOrderCreate model, HttpContext context);
        Task<BaseResponse<PaymentResponseModel>> PaymentExecute(IQueryCollection collections);
    }
}

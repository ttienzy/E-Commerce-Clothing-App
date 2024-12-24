using Application.BLL.Contracts;
using Application.DAL.Domain.Contracts;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Common;
using Application.DAL.Shared.Dtos.ManagerDTO;
using Application.DAL.Shared.Dtos.OrderDtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Services
{
    public class PageManagers : IPageManagers
    {
        private readonly IUnitOfWork _unitOfWork;
        public PageManagers(IUnitOfWork unitOfWorkr)
        {
            _unitOfWork = unitOfWorkr;
        }

        public async Task<BaseResponse<ManagerInfoDto>> InfoForManager()
        {
            try
            {
                var data = await _unitOfWork.tb_Providers.InfoForManager();
                return new BaseResponse<ManagerInfoDto>().Success(data);
            }
            catch (Exception ex)
            {
                return new BaseResponse<ManagerInfoDto>().InternalServerError(ex.Message);
            }
        }

        // Area chart
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

        public async Task<BaseResponse<List<BestSellingProduct>>> SoftByBestSellingProduct()
        {
            try
            {
                var data = await _unitOfWork.tb_Providers.SoftByBestSellingProduct();
                if (data.Count == 0)
                {
                    return new BaseResponse<List<BestSellingProduct>>().NotFound(ErrorMessage.EMPTY_RECORD);
                }
                return new BaseResponse<List<BestSellingProduct>>().Success(data);
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<BestSellingProduct>>().InternalServerError(ex.Message);
            }
        }
    }
}

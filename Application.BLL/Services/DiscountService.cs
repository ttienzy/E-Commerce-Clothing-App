using Application.BLL.Contracts;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Common;
using Application.DAL.Shared.Dtos.DiscountDto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Services
{
    public class DiscountService : IDiscountServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DiscountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponse<string>> CreateDiscountAsync(DiscountCreateDto discountCreateDto)
        {
            try
            {
                var newDiscount = _mapper.Map<Discounts>(discountCreateDto);
                await _unitOfWork.tb_Discounts.AddAsync(newDiscount);
                await _unitOfWork.SaveChangeAsync();

                return new BaseResponse<string>().Success("Created success");
            }
            catch(Exception ex)
            {
                return new BaseResponse<string>().InternalServerError(ex.Message);
            }
        }
        public async Task<BaseResponse<string>> AddDiscountFroProductsAsync(Guid ProductId, Guid DiscountId)
        {
            try
            {
                var product = await _unitOfWork.tb_Products.FindByIdAsync(ProductId);
                if (product is null)
                {
                    return new BaseResponse<string>().NotFound(ErrorMessage.NOT_FOUND_PRODUCT);
                }

                var discount = await _unitOfWork.tb_Discounts.FindByIdAsync(DiscountId);
                if (discount is null)
                {
                    return new BaseResponse<string>().NotFound(ErrorMessage.NOT_FOUND_PRODUCT);
                }


                product.DiscountId = discount.Id;
                _unitOfWork.tb_Products.Update(product);

                await _unitOfWork.SaveChangeAsync();

                return new BaseResponse<string>().Success($"Create discount for {product.Name} success");

            }
            catch (Exception ex)
            {
                return new BaseResponse<string>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<string>> DeleteDiscount(Guid id)
        {
            try
            {
                var discount = await _unitOfWork.tb_Discounts.FindByIdAsync(id);
                if (discount is null)
                {
                    return new BaseResponse<string>().Success(ErrorMessage.RECORD_NOT_FOUND);
                }

                _unitOfWork.tb_Discounts.Delete(discount);
                await _unitOfWork.SaveChangeAsync();
                return new BaseResponse<string>().Success("DeleteDiscount");
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<List<DiscountDataDto>>> GetAsync()
        {
            try
            {
                var data = await _unitOfWork.tb_Discounts.GetDiscountIncludeProductsAsync();
                if (data.Count == 0)
                {
                    return new BaseResponse<List<DiscountDataDto>>().BadRequest(ErrorMessage.EMPTY_RECORD);
                }
                var datadtos = _mapper.Map<List<DiscountDataDto>>(data);

                return new BaseResponse<List<DiscountDataDto>>().Success(datadtos);
            }
            catch(Exception ex)
            {
                return new BaseResponse<List<DiscountDataDto>>().InternalServerError(ex.Message);
            }
        }
        
        public async Task<BaseResponse<DiscountDataDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _unitOfWork.tb_Discounts.FindByIdAsync(id);
                if (result is null)
                {
                    return new BaseResponse<DiscountDataDto>().NotFound(ErrorMessage.RECORD_NOT_FOUND);
                }
                var dtomaps = _mapper.Map<DiscountDataDto>(result);
                return new BaseResponse<DiscountDataDto>().Success(dtomaps);
            }
            catch (Exception ex)
            {
                return new BaseResponse<DiscountDataDto>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<string>> UpdateDiscount(Guid disocuntId, DiscountCreateDto discountCreateDto)
        {
            try
            {
                var discount = await _unitOfWork.tb_Discounts.FindByIdAsync(disocuntId);
                if (discount is null)
                {
                    return new BaseResponse<string>().NotFound(ErrorMessage.RECORD_NOT_FOUND);
                }

                discount.Name = discountCreateDto.Name;
                discount.Discount_percent = discountCreateDto.Discount_percent;
                discount.UpdateAt = discountCreateDto.UpdateAt;
                _unitOfWork.tb_Discounts.Update(discount);


                await _unitOfWork.SaveChangeAsync();

                return new BaseResponse<string>().Success($"Update discount for {discount.Id} success");

            }
            catch (Exception ex)
            {
                return new BaseResponse<string>().InternalServerError(ex.Message);
            }
        }

        
    }
}

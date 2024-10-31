using Application.BLL.Contracts;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using Application.DAL.Domain.QuerryParams;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Common;
using Application.DAL.Shared.Dtos.ProductDto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.BLL.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<Products>> DeleteProduct(Guid id)
        {
            try
            {
                var data = await _unitOfWork.tb_Products.FindByIdAsync(id);
                if (data is null)
                {
                    return new BaseResponse<Products>().NotFound(ErrorMessage.NOT_FOUND_PRODUCT);
                }
                _unitOfWork.tb_Products.Delete(data);
                await _unitOfWork.SaveChangeAsync();
                return new BaseResponse<Products>().Success(data);
            }
            catch (Exception ex)
            {
                return new BaseResponse<Products>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<List<ProductData>>> GetAsync()
        {
            try
            {
                var data = await _unitOfWork.tb_Products.GetAllAsync();
                return new BaseResponse<List<ProductData>>().Success(_mapper.Map<List<ProductData>>(data));
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<ProductData>>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<ProductData>> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await _unitOfWork.tb_Products.FindByIdAsync(id);
                if (data == null)
                {
                    return new BaseResponse<ProductData>().NotFound(ErrorMessage.RECORD_NOT_FOUND);
                }
                return new BaseResponse<ProductData>().Success(_mapper.Map<ProductData>(data));
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductData>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<List<ProductIncludedDiscountDto>>> GetProductIncludedDiscount(ProductQuerryParams productQuerryParams)
        {
            try
            {
                var querry = _unitOfWork.tb_Products.GetProductIncludedDiscount();

                if (productQuerryParams.IsDescending)
                {
                    querry = querry.OrderByDescending(x => x.Name);
                }
                
                var results = await querry.ToListAsync();
                return new BaseResponse<List<ProductIncludedDiscountDto>>().Success(_mapper.Map<List<ProductIncludedDiscountDto>>(results));

            }
            catch(Exception ex)
            {
                return new BaseResponse<List<ProductIncludedDiscountDto>>().InternalServerError(ex.Message);
            }
        }

        public async Task<BaseResponse<List<ProductData>>> GetProductsAsync(int skip, int take)
        {
            try
            {
                var data = await _unitOfWork.tb_Products.GetProductsAsync(skip, take);
                if (data.Count == 0)
                {
                    return new BaseResponse<List<ProductData>>().Success(_mapper.Map<List<ProductData>>(data));
                }
                return new BaseResponse<List<ProductData>>().Success(_mapper.Map<List<ProductData>>(data));
            }
            catch(Exception ex)
            {
                return new BaseResponse<List<ProductData>>().InternalServerError(ex.Message);
            }
        }
    }
}

using Application.BLL.Contracts;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using Application.DAL.Domain.QuerryParams;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Common;
using Application.DAL.Shared.Dtos.ProductDto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
        public async Task<BaseResponse<List<ProductIncludedDiscountDto>>> GetProductByQueryAsync(Guid categoryId,decimal min, decimal max)
        {
            try
            {
                var query = _unitOfWork.tb_Products.ProductAsQuerryAble();
                var cate = await _unitOfWork.tb_Categories.FindByIdAsync(categoryId);
                if (cate is null)
                {
                    if (min != 0 && max != 0)
                    {
                        var x = _mapper.Map<List<ProductIncludedDiscountDto>>(query.Where(x => x.Price >= min && x.Price <= max).Include(x => x.discounts).ToList());
                        return new BaseResponse<List<ProductIncludedDiscountDto>>().Success(x);
                    }
                }
                if (min != 0 && max != 0)
                {
                    query = query.Include(x => x.categories).Include(x => x.discounts).Where(x => x.categories.Id == categoryId && x.Price >= min && x.Price <= max);
                    return new BaseResponse<List<ProductIncludedDiscountDto>>().Success(_mapper.Map<List<ProductIncludedDiscountDto>>(query.ToList()));
                }
                var xy = _mapper.Map<List<ProductIncludedDiscountDto>>(query.Include(x => x.categories).Include(x => x.discounts).Where(x => x.categories.Id == categoryId).ToList());
                return new BaseResponse<List<ProductIncludedDiscountDto>>().Success(xy);
            }
            catch(Exception ex)
            {
                return new BaseResponse<List<ProductIncludedDiscountDto>>().InternalServerError(ex.Message);
            }
        }
        public async Task<BaseResponse<string>> UpdateProduct(Guid idProduct, ProductUpdateDto productUpdateDto)
        {
            try
            {
                var product = await _unitOfWork.tb_Products.FindByIdAsync(idProduct);
                if (product is null)
                    return new BaseResponse<string>().NotFound("Not found");
                product.Name = productUpdateDto.Name;
                product.Quantity = productUpdateDto.Quantity;
                product.Price = productUpdateDto.Price;
                product.DiscountId = productUpdateDto.DiscountId;

                _unitOfWork.tb_Products.Update(product);
                await _unitOfWork.SaveChangeAsync();

                return new BaseResponse<string>().Success("Success");
            }
            catch(Exception ex)
            {
                return new BaseResponse<string>().InternalServerError(ex.Message);
            }
        }
        public async Task<BaseResponse<List<PreviewDto>>> NumberReview(string nameProduct)
        {
            try
            {
                var results = await _unitOfWork.tb_Products.NumberPreview(nameProduct);
                return new BaseResponse<List<PreviewDto>>().Success(results);
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<PreviewDto>>().InternalServerError(ex.Message);
            }
        }
    }
}

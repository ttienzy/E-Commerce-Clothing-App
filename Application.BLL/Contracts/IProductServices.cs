using Application.DAL.Domain.Models;
using Application.DAL.Domain.QuerryParams;
using Application.DAL.Shared.Base;
using Application.DAL.Shared.Dtos.DiscountDto;
using Application.DAL.Shared.Dtos.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL.Contracts
{
    public interface IProductServices
    {
        Task<BaseResponse<List<ProductData>>> GetAsync();
        Task<BaseResponse<ProductData>> GetByIdAsync(Guid id);
        Task<BaseResponse<List<ProductData>>> GetProductsAsync(int skip, int take);
        Task<BaseResponse<List<ProductIncludedDiscountDto>>> GetProductIncludedDiscount(ProductQuerryParams productQuerryParams);
        Task<BaseResponse<Products>> DeleteProduct(Guid id);    
    }
}

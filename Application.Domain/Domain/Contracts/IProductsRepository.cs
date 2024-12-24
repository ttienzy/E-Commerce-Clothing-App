using Application.DAL.Domain.Models;
using Application.DAL.Shared.Dtos.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Contracts
{
    public interface IProductsRepository : IGenericRepository<Products>
    {
        Task<Products?> FindByNameAsync(string name);
        Task<List<Products>> GetProductsAsync(int skip, int take);
        IQueryable<Products> GetProductIncludedDiscount();
        IQueryable<Products> ProductAsQuerryAble();
        Task<List<PreviewDto>> NumberPreview(string nameProduct);
    }
}

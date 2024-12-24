using Application.DAL.DbContextData;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using Application.DAL.Shared.Dtos.ProductDto;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Repositories
{
    public class ProductsRepository : GenericRepositoryBase<Products>, IProductsRepository
    {
        public ProductsRepository(dbContext context) : base(context)
        {
        }

        public async Task<Products?> FindByNameAsync(string name)
        {
            return await _context.products.FirstOrDefaultAsync(x => x.Name == name);
        }

        public IQueryable<Products> GetProductIncludedDiscount()
        {
            return _context.products.Include(x => x.discounts).AsQueryable();
        }

        public async Task<List<Products>> GetProductsAsync(int skip,int take)
        {
            return await _context.products.Skip(skip-1).Take(take).ToListAsync();
        }
        public IQueryable<Products> ProductAsQuerryAble()
        {
            return _context.products.AsQueryable();
        }
        public async Task<List<PreviewDto>> NumberPreview(string nameProduct)
        {
            var results = from p in _context.products 
                          join ot in _context.orderItems on p.Id equals ot.ProductId
                          join o in _context.orders on ot.OrderId equals o.Id
                          join u in _context.Users on o.UserId equals u.Id
                          where p.Name == nameProduct && o.Description != "" 
                          select new PreviewDto
                          {
                              UserName = u.UserName,
                              Description = o.Description,
                              DateCreated = o.UpdatedAt
                          };
            return await results.ToListAsync();

        }
    }
}

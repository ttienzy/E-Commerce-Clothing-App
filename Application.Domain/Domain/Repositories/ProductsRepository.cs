using Application.DAL.DbContextData;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
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
    }
}

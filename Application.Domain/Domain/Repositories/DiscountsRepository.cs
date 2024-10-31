using Application.DAL.DbContextData;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Repositories
{
    public class DiscountsRepository : GenericRepositoryBase<Discounts>, IDiscountsRepository
    {
        public DiscountsRepository(dbContext context) : base(context)
        {
        }

        public async Task<List<Discounts>> GetDiscountIncludeProductsAsync()
        {
            return await _context.discounts.Include(f => f.products).ToListAsync();
        }
    }
}

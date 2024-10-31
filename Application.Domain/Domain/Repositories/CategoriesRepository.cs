using Application.DAL.DbContextData;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Repositories
{
    public class CategoriesRepository : GenericRepositoryBase<Categories>, ICategoriesRepository
    {
        public CategoriesRepository(dbContext context) : base(context)
        {
        }
        public async Task<List<Categories>> GetCategoryIncludeAsync()
        {
            return await _context.categories.Include(p => p.products).ToListAsync();
        }
    }
}

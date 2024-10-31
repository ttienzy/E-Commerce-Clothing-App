using Application.DAL.DbContextData;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Repositories
{
    public class InventoriesRepository : GenericRepositoryBase<Inventories>, IIventoriesRepository
    {
        public InventoriesRepository(dbContext context) : base(context)
        {
        }

        public async Task<List<Inventories>> GetInventoryIncludeAsync()
        {
            return await _context.inventories.Include(p => p.products).ToListAsync();
        }
    }
}

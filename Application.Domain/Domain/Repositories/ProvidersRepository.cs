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
    public class ProvidersRepository : GenericRepositoryBase<Providers>, IProvidersRepository
    {
        public ProvidersRepository(dbContext context) : base(context)
        {
        }

        public async Task<Providers?> FindByTelNoAsync(string TelNo)
        {
            return await _context.providers.FirstOrDefaultAsync(e => e.TelNo.Equals(TelNo));
        }
    }
}

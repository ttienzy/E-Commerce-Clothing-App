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
    public class UserAddressRepository : GenericRepositoryBase<UserAddress>, IUserAddressRepository
    {
        public UserAddressRepository(dbContext context) : base(context)
        {
        }

        public Task<UserAddress> FindAddressByUserId(Guid userId)
        {
            var data = _context.userAddresses.FirstOrDefaultAsync(x => x.UserId == userId);
            return data;
        }
    }
}

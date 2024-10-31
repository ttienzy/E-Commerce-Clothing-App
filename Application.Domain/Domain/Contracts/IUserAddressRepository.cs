using Application.DAL.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Contracts
{
    public interface IUserAddressRepository : IGenericRepository<UserAddress>
    {
        Task<UserAddress> FindAddressByUserId(Guid userId);
    }
}

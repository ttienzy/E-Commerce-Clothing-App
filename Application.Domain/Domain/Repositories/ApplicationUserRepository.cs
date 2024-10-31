using Application.DAL.DbContextData;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using Application.DAL.Shared.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Repositories
{
    public class ApplicationUserRepository : GenericRepositoryBase<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(dbContext context) : base(context)
        {
        }

        
    }
}

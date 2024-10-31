using Application.DAL.DbContextData;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Repositories
{
    public class OrderPaymentsRepository : GenericRepositoryBase<OrderPayment>, IOrderPaymentsRepository
    {
        public OrderPaymentsRepository(dbContext context) : base(context)
        {
        }
    }
}

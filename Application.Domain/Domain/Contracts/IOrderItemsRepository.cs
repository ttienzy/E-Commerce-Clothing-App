﻿using Application.DAL.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Contracts
{
    public interface IOrderItemsRepository : IGenericRepository<OrderItems>
    {
        Task<OrderItems?> FindByOrderId(Guid orderId);
    }
}

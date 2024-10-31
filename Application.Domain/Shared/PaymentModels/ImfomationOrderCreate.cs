﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.PaymentModels
{
    public class ImfomationOrderCreate
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
    }
}

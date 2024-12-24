using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Dtos.OrderDtos
{
    public class RevenueDto
    {
        public int Month { get; set; }
        public decimal Revenue { get; set; }
        public decimal Profit { get; set; }
    }
}

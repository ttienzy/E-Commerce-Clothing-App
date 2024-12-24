using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Dtos.ProductDto
{
    public class ProductShipDto
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public int quatity { get; set; }
        public decimal price { get; set; }
    }
}

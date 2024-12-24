using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Dtos.CartDTO
{
    public class CartDataDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string? ImageProducts { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountCode { get; set; }
        public Guid? CategoryId { get; set; }
    }
}

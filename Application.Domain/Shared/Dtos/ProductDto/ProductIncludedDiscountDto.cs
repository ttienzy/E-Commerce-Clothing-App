using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Dtos.ProductDto
{
    public class ProductIncludedDiscountDto
    {
        public Guid Id { get; set; }
        public string? ImageProducts { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPercent { get; set; }
        public Guid? CategoryId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Models
{
    public class Products
    {
        public Guid Id { get; set; }
        public string? ImageProducts { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; } = string.Empty;
        public List<OrderItems> orderItems { get; set; }
        public List<ReceiptItems> receiptItems { get; set; }
        public List<Cart> carts { get; set; }
        public Guid? DiscountId { get; set; }
        public Discounts discounts { get; set; }
        public Guid? InventoryId { get; set; }
        public Inventories inventories { get; set; }
        public Guid? CategoryId { get; set; }
        public Categories categories { get; set; }
        public Products()
        {
            this.Id = Guid.NewGuid();
        }
    }
}

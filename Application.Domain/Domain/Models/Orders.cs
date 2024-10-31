using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Models
{
    public class Orders
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } 
        public decimal TotalOrderMoney { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public ApplicationUser applicationUser { get; set; }
        public List<OrderItems> orderItems { get; set; }
        public OrderPayment payments { get; set; }

        public Orders()
        {
            this.Id = Guid.NewGuid();
            this.CreatedAt = DateTime.Now;
        }
    }
}

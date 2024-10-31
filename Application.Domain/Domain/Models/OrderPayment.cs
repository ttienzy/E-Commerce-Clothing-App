using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Models
{
    public class OrderPayment
    {
        public Guid Id { get; set; } 
        public string? ProviderPayment { get; set; }
        public decimal Amount { get; set; }
        public string? OrderInfo { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; } = default!;
        public Guid OrderId { get; set; }
        public Orders orders { get; set; }
        public OrderPayment()
        {
            this.Id = Guid.NewGuid();
        }
    }
}

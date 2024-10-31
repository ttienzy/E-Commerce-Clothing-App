using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Models
{
    public class OrderItems
    {
        public Guid Id { get; set; }
        public int QuantityProductOrder { get; set; }
        public decimal Price { get; set; }
        public Guid OrderId { get; set; }
        public Orders orders { get; set; }
        public Guid ProductId { get; set; }
        public Products products { get; set; }

        public OrderItems()
        {
            this.Id = Guid.NewGuid();
        }
    }
}

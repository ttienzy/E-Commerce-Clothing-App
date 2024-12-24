using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser user { get; set; }
        public Guid ProductId { get; set; }
        public Products product { get; set; }
        public Cart()
        {
            Id = Guid.NewGuid();
        }
    }
}

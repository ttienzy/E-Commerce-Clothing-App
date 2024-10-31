using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Models
{
    public class Inventories
    {
        public Guid Id { get; set; } 
        public int? Quantity { get; set; }
        public DateTime? CreateAt { get; set; } 
        public DateTime? UpdateAt { get; set; } = default!;
        public List<Products> products { get; set; }
        public Inventories()
        {
            this.Id = Guid.NewGuid();
            this.CreateAt = DateTime.Now;
        }
    }
}

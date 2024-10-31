using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Models
{
    public class Discounts
    {
        public Guid Id { get; set; } 
        public string? Name { get; set; } = string.Empty;
        public decimal? Discount_percent { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreateAt { get; set; } 
        public DateTime? UpdateAt { get; set; } = default!;
        public List<Products> products { get; set; }
        public Discounts()
        {
            this.Id = Guid.NewGuid();
            this.CreateAt = DateTime.Now;
            this.Active = true;
        }

    }
}

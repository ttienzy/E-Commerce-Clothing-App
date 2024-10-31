using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Dtos.DiscountDto
{
    public class DiscountCreateDto
    {
        public Guid Id  = Guid.NewGuid();
        [Required]
        public string? Name { get; set; } = string.Empty;
        [Required]
        public decimal? Discount_percent { get; set; }
        public bool? Active = true;
        public DateTime? CreateAt = DateTime.Now;
        public DateTime? UpdateAt = DateTime.Now;
    }
}

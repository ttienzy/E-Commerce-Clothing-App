using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Dtos.InventoryDto
{
    public class InventoryCreateDto
    {
        public Guid Id = Guid.NewGuid();
        public int? Quantity { get; set; }
        public DateTime? CreateAt = DateTime.Now;
        public DateTime? UpdateAt = DateTime.Now;
    }
}

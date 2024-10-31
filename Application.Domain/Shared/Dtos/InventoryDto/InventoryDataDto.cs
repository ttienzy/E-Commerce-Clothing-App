using Application.DAL.Domain.Models;
using Application.DAL.Shared.Dtos.InfoProviderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Dtos.InventoryDto
{
    public class InventoryDataDto
    {
        public int? Quantity { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; } 
        public List<ProductInventory> products { get; set; }
    }
}

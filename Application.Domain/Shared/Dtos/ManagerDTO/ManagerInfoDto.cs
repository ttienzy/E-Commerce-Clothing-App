using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Dtos.ManagerDTO
{
    public class ManagerInfoDto
    {
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public int TotalUsers { get; set; }
        public int TotalCategories { get; set; }
        public int TotalDiscounts { get; set; }
        public int TotalOrderStatus { get; set; }
        public int TotalInventory { get; set; }
        public int OnlineOrders { get; set; }
        public int OfflineOrders { get; set; }
    }
}

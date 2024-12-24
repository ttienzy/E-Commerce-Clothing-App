using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Dtos.OrderDtos
{
    public class OrderHistoryDto
    {
        public Guid PaymentId { get; set; }
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalMoney { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ProductName { get; set; }
        public string UrlImage { get; set; }
        public string Status { get; set; }
    }
}

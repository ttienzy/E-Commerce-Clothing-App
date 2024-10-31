using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Models
{
    public class ReceiptItems
    {
        public Guid Id { get; set; } 
        public Guid ReceiptId { get; set; }
        public Receipts receipts { get; set; }
        public Guid ProductId { get; set; }
        public Products products { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public ReceiptItems()
        {
            this.Id = Guid.NewGuid();
        }
    }
}

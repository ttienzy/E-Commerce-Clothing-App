using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Models
{
    public class Receipts
    {
        public Guid Id { get; set; }
        public decimal TotalReceiptMoney { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ProviderId { get; set; }
        public Providers providers { get; set; }
        public List<ReceiptItems> receiptItems { get; set; }
        public Receipts()
        {
            this.Id = Guid.NewGuid();
            this.CreatedAt = DateTime.Now;
        }
    }
}

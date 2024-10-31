using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Dtos.InfoProviderDto
{
    public class InfoTransactionProviderDto
    {
        [Phone]
        public string TelNo { get; set; } = string.Empty;
        public string NameProduct { get; set; } = string.Empty;
        public IFormFile? formFile { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Guid DiscountId { get; set; }
        public Guid CategoryId { get; set; }
    }
}

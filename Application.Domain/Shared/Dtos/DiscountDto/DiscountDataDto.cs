using Application.DAL.Domain.Models;
using Application.DAL.Shared.Dtos.InfoProviderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Dtos.DiscountDto
{
    public class DiscountDataDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public decimal? Discount_percent { get; set; }
        //public bool? Active { get; set; }
        //public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        //public List<ProductDiscountDto> products { get; set; }
    }
}

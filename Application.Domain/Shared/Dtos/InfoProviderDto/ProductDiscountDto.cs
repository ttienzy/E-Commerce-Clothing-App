using Application.DAL.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Dtos.InfoProviderDto
{
    public class ProductDiscountDto
    {
        public Guid Id { get; set; }
        public string NameProduct { get; set; } = string.Empty;
    }
}

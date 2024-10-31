using Application.DAL.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Dtos.InfoProviderDto
{
    public class ProviderDataDto
    {
        public string NameProvider { get; set; } = string.Empty;
        [Phone]
        public string TelNo { get; set; } = string.Empty;
        //public List<Receipts> receipts { get; set; }
    }
}

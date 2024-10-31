using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Models
{
    public class Providers
    {
        public Guid Id { get; set; } 
        public string NameProvider { get; set; } = string.Empty;
        public string TelNo { get; set; } = string.Empty;
        public List<Receipts> receipts { get; set; }
        public Providers()
        {
            this.Id = Guid.NewGuid();
        }
    }
}

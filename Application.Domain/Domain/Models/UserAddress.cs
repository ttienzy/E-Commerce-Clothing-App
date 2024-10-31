using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Models
{
    public class UserAddress
    {
        public Guid Id { get; set; } 
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? Ward {  get; set; }
        public Guid? UserId { get; set; }
        public ApplicationUser applicationUser { get; set; }
        public UserAddress()
        {
            this.Id = Guid.NewGuid();
        }
    }
}

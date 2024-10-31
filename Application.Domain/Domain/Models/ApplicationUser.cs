using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public DateTime Create_At { get; set; }
        public DateTime Modified_At { get; set; }
        public UserAddress address { get; set; }
        public List<Orders> orders { get; set; }

        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.Create_At = DateTime.Now;
            this.Modified_At = DateTime.Now;
        }
    }
}

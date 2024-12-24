using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Dtos.AuthenticationDto
{
    public class SetInfoDto
    {
        public string UserName {  get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime UpdatedAt = DateTime.Now;

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Dtos.AuthenticationDto
{
    public class RegisterDto
    {

        public string UserName { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }

        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
    }
}

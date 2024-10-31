using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Dtos.AuthenticationDto
{
    public class ForgotPasswordDTO
    {
        [Required]
        public string Email { get; set; }
    }
}

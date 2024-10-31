using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.UserDto
{
    public class GetInfomationUserDto
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward {  get; set; }
        public string Role { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Dtos.CartDTO
{
    public class AddCartDto
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
    }
}

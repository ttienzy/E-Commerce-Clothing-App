using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Dtos.CategoryDto
{
    public class CategoryDto
    {
        public Guid Id = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; } 
    }
}

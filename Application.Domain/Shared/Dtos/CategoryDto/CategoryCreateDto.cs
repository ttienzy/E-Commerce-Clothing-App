using Application.DAL.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Dtos.CategoryDto
{
    public class CategoryCreateDto
    {
        public Guid Id = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? CreatedAt = DateTime.Now;
        public DateTime? UpdatedAt = DateTime.Now;
    }
}

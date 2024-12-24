using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Dtos.ProductDto
{
    public class PreviewDto
    {
        public string UserName { get; set; }
        public string Description { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}

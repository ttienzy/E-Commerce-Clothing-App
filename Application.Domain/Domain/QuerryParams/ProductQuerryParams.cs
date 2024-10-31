using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Domain.QuerryParams
{
    public class ProductQuerryParams
    {
        public bool IsDescending { get; set; }
        public int PageNumber { get; set; } 
        public int PageSize { get; set; } 
    }
}

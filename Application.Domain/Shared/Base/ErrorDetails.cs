using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Base
{
    public class ErrorDetails
    {
        public HttpStatusCode status {  get; set; }
        public string message { get; set; }
    }
}

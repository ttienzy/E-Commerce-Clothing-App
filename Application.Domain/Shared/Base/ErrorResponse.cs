using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Base
{
    public class ErrorResponse<T> : IActionResult
    {
        public bool success { get; set; } = false;
        public HttpStatusCode statusCode { get; set; }

        public string errorMessage { get; set; } = default!;
        //public BaseResponse<T> response { get; set; }
        public ErrorResponse() { }


        //public ErrorResponse(BaseResponse<T> response)
        //{
        //    this.statusCode = response.statusCode;
        //    this.errorMessage = response.message;
        //}

        public ErrorResponse<T> Error(BaseResponse<T> response)
        {
            this.statusCode = response.statusCode;
            this.errorMessage = response.message;
            return this;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(this), Encoding.UTF8);
            return Task.FromResult(context);
        }
    }
}

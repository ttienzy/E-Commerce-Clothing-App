using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Shared.Base
{
    public class BaseResponse<T>
    {
        public bool success { get; set; } = false;
        public HttpStatusCode statusCode { get; set; }
        public string message { get; set; } = default!;
        public T response { get; set; }

        /// <summary>
        /// Handle success reponse from service
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public BaseResponse<T> Success(T response)
        {
            this.success = true;
            this.response = response;
            this.statusCode = HttpStatusCode.OK;
            return this;
        }

        

        /// <summary>
        /// Handle BadRequest reponse from service
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public BaseResponse<T> BadRequest(string message)
        {
            this.message = message;
            this.statusCode = HttpStatusCode.BadRequest;
            return this;
        }

        /// <summary>
        /// Handle Unauthorized response from service
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public BaseResponse<T> Unauthorized(string message)
        {
            this.message = message;
            this.statusCode = HttpStatusCode.Unauthorized;
            return this;
        }

        /// <summary>
        /// Handle Not Found response from service
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public BaseResponse<T> NotFound(string message)
        {
            this.message = message;
            this.statusCode = HttpStatusCode.NotFound;
            return this;
        }

        /// <summary>
        /// Handle Forbidden reponse from service
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public BaseResponse<T> Forbidden(string message)
        {
            this.message = message;
            this.statusCode = HttpStatusCode.Forbidden;
            return this;
        }

        /// <summary>
        /// Handle InternalServerError reponse from service
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public BaseResponse<T> InternalServerError(string message)
        {
            this.message = message;
            this.statusCode = HttpStatusCode.InternalServerError;
            return this;
        }
    }
}

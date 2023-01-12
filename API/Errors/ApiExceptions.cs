using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiExceptions
    {
        
        public ApiExceptions(int statusCode,string message, string details) 
        {
            this.StatusCode = statusCode;
            this.Details = details;
            this.Message = message;
   
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
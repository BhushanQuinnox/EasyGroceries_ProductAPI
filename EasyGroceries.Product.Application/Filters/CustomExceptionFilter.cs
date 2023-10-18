using EasyGroceries.Product.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EasyGroceries.Product.Application.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            ResponseDto<Exception> response = new ResponseDto<Exception>()
            {
                IsSuccess = false,
                Message = filterContext.Exception.Message,
                Result = filterContext.Exception,
                Status = (int)HttpStatusCode.InternalServerError
            };

            filterContext.Result = new JsonResult(response);
        }
    }
}

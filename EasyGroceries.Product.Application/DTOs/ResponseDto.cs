using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EasyGroceries.Product.Application.DTOs
{
    public class ResponseDto<T>
    {
        public T Result { get; set; }
        public int Status { get; set; } = (int)HttpStatusCode.OK;
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}

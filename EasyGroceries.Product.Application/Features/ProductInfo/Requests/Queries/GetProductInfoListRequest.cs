using EasyGroceries.Product.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGroceries.Product.Application.Features.ProductInfo.Requests.Queries
{
    public class GetProductInfoListRequest : IRequest<List<ProductInfoDto>>
    {
    }
}

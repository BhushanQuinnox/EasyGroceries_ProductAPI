using EasyGroceries.Product.Application.Contracts.Services;
using EasyGroceries.Product.Application.DTOs;
using EasyGroceries.Product.Application.Features.ProductInfo.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGroceries.Product.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMediator _mediator;

        public ProductService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ResponseDto<ProductInfoDto>> GetProductDetails(int id)
        {
            ResponseDto<ProductInfoDto> response = new ResponseDto<ProductInfoDto>();

            try
            {
                var productInfo = await _mediator.Send(new GetProductInfoRequest() { Id = id });
                response.Result = productInfo;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto<List<ProductInfoDto>>> GetProductList()
        {
            ResponseDto<List<ProductInfoDto>> response = new ResponseDto<List<ProductInfoDto>>();

            try
            {
                var productInfos = await _mediator.Send(new GetProductInfoListRequest());
                response.Result = productInfos;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}

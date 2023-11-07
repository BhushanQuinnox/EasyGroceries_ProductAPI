using System.Net;
using EasyGroceries.Product.Application.Contracts.Services;
using EasyGroceries.Product.Application.DTOs;
using EasyGroceries.Product.Application.Features.ProductInfo.Requests.Queries;
using MediatR;

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
            var productInfo = await _mediator.Send(new GetProductInfoRequest() { Id = id });
            if (productInfo == null)
            {
                response.Message = $"No product found of id {id}";
                response.Status = (int)HttpStatusCode.NotFound;
                response.IsSuccess = false;
            }

            response.Result = productInfo;
            return response;
        }

        public async Task<ResponseDto<List<ProductInfoDto>>> GetProductList()
        {
            ResponseDto<List<ProductInfoDto>> response = new ResponseDto<List<ProductInfoDto>>();
            var productInfos = await _mediator.Send(new GetProductInfoListRequest());
            response.Result = productInfos;
            return response;
        }
    }
}

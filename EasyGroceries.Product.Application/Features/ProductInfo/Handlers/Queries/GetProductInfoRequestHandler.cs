using AutoMapper;
using EasyGroceries.Product.Application.Contracts.Infrastructure;
using EasyGroceries.Product.Application.DTOs;
using EasyGroceries.Product.Application.Features.ProductInfo.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGroceries.Product.Application.Features.ProductInfo.Handlers.Queries
{
    public class GetProductInfoRequestHandler : IRequestHandler<GetProductInfoRequest, ProductInfoDto>
    {
        private readonly IProductInfoRepository _productInfoRepository;
        private readonly IMapper _mapper;

        public GetProductInfoRequestHandler(IProductInfoRepository productInfoRepository, IMapper mapper)
        {
            _productInfoRepository = productInfoRepository;
            _mapper = mapper;
        }

        public async Task<ProductInfoDto> Handle(GetProductInfoRequest request, CancellationToken cancellationToken)
        {
            var productInfo = await _productInfoRepository.GetProductInfoById(request.Id);
            return _mapper.Map<ProductInfoDto>(productInfo);
        }
    }
}

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
    public class GetProductInfoListRequestHandler : IRequestHandler<GetProductInfoListRequest, List<ProductInfoDto>>
    {
        private readonly IProductInfoRepository _productInfoRepository;
        private readonly IMapper _mapper;

        public GetProductInfoListRequestHandler(IProductInfoRepository productInfoRepository, IMapper mapper)
        {
            _productInfoRepository = productInfoRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductInfoDto>> Handle(GetProductInfoListRequest request, CancellationToken cancellationToken)
        {
            var productInfoList = await _productInfoRepository.GetAllProductsInfo();
            return _mapper.Map<List<ProductInfoDto>>(productInfoList);
        }
    }
}

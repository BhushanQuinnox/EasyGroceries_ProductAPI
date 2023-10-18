using EasyGroceries.Product.Application.Contracts.Services;
using EasyGroceries.Product.Application.DTOs;
using EasyGroceries.Product.Application.Features.ProductInfo.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyGroceries.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ResponseDto<List<ProductInfoDto>>> GetProducts()
        {
            var response = await _productService.GetProductList();
            if (!response.IsSuccess)
            {
                response.Status = StatusCodes.Status500InternalServerError;
            }
            else
            {
                response.Status = StatusCodes.Status200OK;
            }

            return response;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ResponseDto<ProductInfoDto>> GetProductById(int id)
        {
            var response = await _productService.GetProductDetails(id);
            if (!response.IsSuccess)
            {
                response.Status = StatusCodes.Status500InternalServerError;
            }
            else
            {
                response.Status = StatusCodes.Status200OK;
            }

            return response;
        }
    }
}

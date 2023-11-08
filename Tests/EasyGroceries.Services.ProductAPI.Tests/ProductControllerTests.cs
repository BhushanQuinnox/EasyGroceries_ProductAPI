using EasyGroceries.Services.ProductAPI.Controllers;
using EasyGroceries.Product.Application.Contracts.Services;
using EasyGroceries.Product.Application.DTOs;
using System.Net;

namespace EasyGroceries.Services.ProductAPI.Tests;

public class ProductControllerTests
{
    private readonly Mock<IProductService> _productServiceMock;

    public ProductControllerTests()
    {
        _productServiceMock = new Mock<IProductService>();
    }

    private List<ProductInfoDto> InitializeProductData()
    {
        var productInfoDtoLst = new List<ProductInfoDto>
        {
            new ProductInfoDto(){ProductId = 100, Name = "Dove", Price = 45, Category = "Cosmetics", Description = "Soft soap"},
            new ProductInfoDto(){ProductId = 101, Name = "Parle G", Price = 10, Category = "Bakery", Description = "G Genius"},
            new ProductInfoDto(){ProductId = 102, Name = "Potato", Price = 20, Category = "Vegitable", Description = "Fresh Veg"},
            new ProductInfoDto(){ProductId = 103, Name = "Cheese", Price = 110, Category = "Dairy", Description = "Cheezy"},
            new ProductInfoDto(){ProductId = 104, Name = "Milk", Price = 70, Category = "Dairy", Description = "Full cream milk"},
        };

        return productInfoDtoLst;
    }

    [Fact]
    public async void GetProducts_Should_ReturnsAllProductList()
    {
        // Arrange
        var dummyProductData = InitializeProductData();
        ResponseDto<List<ProductInfoDto>> expectedResponse = new ResponseDto<List<ProductInfoDto>>()
        {
            Result = dummyProductData
        };

        _productServiceMock.Setup(x => x.GetProductList()).ReturnsAsync(expectedResponse);

        // Act
        ProductController productController = new ProductController(_productServiceMock.Object);
        var response = await productController.GetProducts();

        // Assert
        Assert.NotNull(response.Result);
        Assert.Equal(expectedResponse.Result, response.Result);
        Assert.Equal((int)HttpStatusCode.OK, response.Status);
        Assert.True(response.IsSuccess);
    }

    [Fact]
    public async void GetProductById_Should_ReturnsProductDetailsOfSpecifiedId()
    {
        // Arrange
        var dummyProductData = InitializeProductData();
        ResponseDto<ProductInfoDto> expectedResponse = new ResponseDto<ProductInfoDto>()
        {
            Result = dummyProductData.FirstOrDefault(x => x.ProductId == 103)
        };

        _productServiceMock.Setup(x => x.GetProductDetails(It.IsAny<int>())).ReturnsAsync(expectedResponse);

        // Act
        ProductController productController = new ProductController(_productServiceMock.Object);
        var response = await productController.GetProductById(103);

        // Assert
        Assert.NotNull(response.Result);
        Assert.Equal(expectedResponse.Result, response.Result);
        Assert.Equal((int)HttpStatusCode.OK, response.Status);
        Assert.True(response.IsSuccess);
    }
}
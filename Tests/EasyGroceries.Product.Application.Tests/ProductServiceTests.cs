using AutoMapper;
using EasyGroceries.Product.Application.Contracts.Infrastructure;
using EasyGroceries.Product.Application.DTOs;
using EasyGroceries.Product.Application.Features.ProductInfo.Handlers.Queries;
using EasyGroceries.Product.Application.Features.ProductInfo.Requests.Queries;
using EasyGroceries.Product.Application.Filters;
using EasyGroceries.Product.Application.Profiles;
using EasyGroceries.Product.Application.Services;
using EasyGroceries.Product.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace EasyGroceries.Product.Application.Tests;

public class ProductServiceTests
{
    private readonly Mock<IMediator> _mediatorMock;

    private List<ProductInfoDto> _productInfoDtoLst;

    public ProductServiceTests()
    {
        _mediatorMock = new Mock<IMediator>();
    }

    private void InitializeProductData()
    {
        _productInfoDtoLst = new List<ProductInfoDto>
        {
            new ProductInfoDto(){ProductId = 100, Name = "Dove", Price = 45, Category = "Cosmetics", Description = "Soft soap"},
            new ProductInfoDto(){ProductId = 101, Name = "Parle G", Price = 10, Category = "Bakery", Description = "G Genius"},
            new ProductInfoDto(){ProductId = 102, Name = "Potato", Price = 20, Category = "Vegitable", Description = "Fresh Veg"},
            new ProductInfoDto(){ProductId = 103, Name = "Cheese", Price = 110, Category = "Dairy", Description = "Cheezy"},
            new ProductInfoDto(){ProductId = 104, Name = "Milk", Price = 70, Category = "Dairy", Description = "Full cream milk"},
        };
    }

    [Fact]
    public async Task GetProductDetails_Should_ReturnsRequestedIdProductInfo()
    {
        // Arrange
        InitializeProductData();
        ProductService productService = new ProductService(_mediatorMock.Object);
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetProductInfoRequest>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(_productInfoDtoLst.Find(x => x.ProductId == 103));

        // Act
        var result = await productService.GetProductDetails(103);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Cheese", result.Result.Name);
    }

    [Fact]
    public async Task GetProductDetails_Should_ReturnsException()
    {
        // Arrange
        InitializeProductData();
        ProductService productService = new ProductService(_mediatorMock.Object);
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetProductInfoRequest>(), It.IsAny<CancellationToken>()))
                    .ThrowsAsync(new Exception());

        // Act
        var result = await productService.GetProductDetails(200);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Null(result.Result);
        Assert.Equal("Exception of type 'System.Exception' was thrown.", result.Message);
    }

    [Fact]
    public async Task GetProductList_Should_ReturnsAllProductInfo()
    {
        // Arrange
        InitializeProductData();
        ProductService productService = new ProductService(_mediatorMock.Object);
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetProductInfoListRequest>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(_productInfoDtoLst);

        // Act
        var result = await productService.GetProductList();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(_productInfoDtoLst.Count, result.Result.Count);
        Assert.Equivalent(_productInfoDtoLst, result.Result);
    }

    [Fact]
    public async Task GetProductList_Should_ReturnsException()
    {
        // Arrange
        InitializeProductData();
        ProductService productService = new ProductService(_mediatorMock.Object);
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetProductInfoListRequest>(), It.IsAny<CancellationToken>()))
                    .ThrowsAsync(new Exception());

        // Act
        var result = await productService.GetProductList();

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Null(result.Result);
        Assert.Equal("Exception of type 'System.Exception' was thrown.", result.Message);
    }

    // [Fact]
    // public void TestExceptionFilter()
    // {
    //     var actionContext = new ActionContext()
    //     {
    //         HttpContext = new DefaultHttpContext(),
    //         RouteData = new RouteData(),
    //         ActionDescriptor = new ActionDescriptor()
    //     };

    //     var mockException = new Mock<Exception>();

    //     mockException.Setup(e => e.StackTrace)
    //       .Returns("Test stacktrace");
    //     mockException.Setup(e => e.Message)
    //       .Returns("Test message");
    //     mockException.Setup(e => e.Source)
    //       .Returns("Test source");

    //     var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
    //     {
    //         Exception = mockException.Object
    //     };

    //     var filter = new CustomExceptionFilter();

    //     filter.OnException(exceptionContext);

    //     var result = exceptionContext.Result;
    // }

}
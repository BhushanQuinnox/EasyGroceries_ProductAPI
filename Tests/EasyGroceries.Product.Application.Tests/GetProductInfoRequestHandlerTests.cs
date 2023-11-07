using AutoMapper;
using EasyGroceries.Product.Application.Contracts.Infrastructure;
using EasyGroceries.Product.Application.DTOs;
using EasyGroceries.Product.Application.Features.ProductInfo.Handlers.Queries;
using EasyGroceries.Product.Application.Features.ProductInfo.Requests.Queries;
using EasyGroceries.Product.Application.Profiles;
using EasyGroceries.Product.Domain;

namespace EasyGroceries.Product.Application.Tests;

public class GetProductInfoRequestHandlerTests
{
    private readonly Mock<IProductInfoRepository> _productRepositoryMock;

    private List<ProductInfo> _productInfoLst;

    public GetProductInfoRequestHandlerTests()
    {
        _productRepositoryMock = new Mock<IProductInfoRepository>();
    }

    private void InitializeProductData()
    {
        _productInfoLst = new List<ProductInfo>
        {
            new ProductInfo(){ProductId = 100, Name = "Dove", Price = 45, Category = "Cosmetics", Description = "Soft soap"},
            new ProductInfo(){ProductId = 101, Name = "Parle G", Price = 10, Category = "Bakery", Description = "G Genius"},
            new ProductInfo(){ProductId = 102, Name = "Potato", Price = 20, Category = "Vegitable", Description = "Fresh Veg"},
            new ProductInfo(){ProductId = 103, Name = "Cheese", Price = 110, Category = "Dairy", Description = "Cheezy"},
            new ProductInfo(){ProductId = 104, Name = "Milk", Price = 70, Category = "Dairy", Description = "Full cream milk"},
        };
    }

    [Fact]
    public async Task Handle_Should_ReturnsRequestedIdProductInfo()
    {
        // Arrange
        InitializeProductData();
        int productInfoId = 103;
        var command = new GetProductInfoRequest() { Id = productInfoId };
        var productInfo = _productInfoLst.FirstOrDefault(x => x.ProductId == productInfoId);

        _productRepositoryMock.Setup(x => x.GetProductInfoById(productInfoId))
                .ReturnsAsync(productInfo);

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        var handler = new GetProductInfoRequestHandler(_productRepositoryMock.Object, mockMapper.CreateMapper());

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.ProductId.Equals(103);
        result.Name.Equals("Cheese");
        result.Price.Equals(110);
        result.Category.Equals("Dairy");
        result.Description.Equals("Cheezy");
    }

    [Fact]
    public async Task Handle_Should_ReturnsNullProductInfo()
    {
        // Arrange
        InitializeProductData();
        int productInfoId = 200;
        var command = new GetProductInfoRequest() { Id = productInfoId };
        var productInfo = _productInfoLst.FirstOrDefault(x => x.ProductId == productInfoId);

        _productRepositoryMock.Setup(x => x.GetProductInfoById(productInfoId))
                .ReturnsAsync(productInfo);

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        var handler = new GetProductInfoRequestHandler(_productRepositoryMock.Object, mockMapper.CreateMapper());

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.Null(result);
    }

}
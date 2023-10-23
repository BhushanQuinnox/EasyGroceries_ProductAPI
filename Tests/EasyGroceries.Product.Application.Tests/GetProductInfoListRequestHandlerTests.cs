using AutoMapper;
using EasyGroceries.Product.Application.Contracts.Infrastructure;
using EasyGroceries.Product.Application.DTOs;
using EasyGroceries.Product.Application.Features.ProductInfo.Handlers.Queries;
using EasyGroceries.Product.Application.Features.ProductInfo.Requests.Queries;
using EasyGroceries.Product.Application.Profiles;
using EasyGroceries.Product.Domain;

namespace EasyGroceries.Product.Application.Tests;

public class GetProductInfoListRequestHandlerTests
{
    private readonly Mock<IProductInfoRepository> _productRepositoryMock;

    private List<ProductInfo> _productInfoLst;

    public GetProductInfoListRequestHandlerTests()
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
    public async Task Handle_Should_ReturnListOfProductInfo()
    {
        // Arrange
        InitializeProductData();
        List<ProductInfoDto> productInfoDtoLst = new List<ProductInfoDto>();
        var command = new GetProductInfoListRequest();

        _productRepositoryMock.Setup(x => x.GetAllProductsInfo()).ReturnsAsync(_productInfoLst);

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        var handler = new GetProductInfoListRequestHandler(_productRepositoryMock.Object, mockMapper.CreateMapper());

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.Count().Equals(_productInfoLst.Count);
    }

    [Fact]
    public async Task Handle_Should_ReturnEmptyListOfProductInfo()
    {
        // Arrange
        List<ProductInfo> productInfoList = new List<ProductInfo>();
        List<ProductInfoDto> productInfoDtoLst = new List<ProductInfoDto>();
        var command = new GetProductInfoListRequest();

        _productRepositoryMock.Setup(x => x.GetAllProductsInfo()).ReturnsAsync(productInfoList);

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        var handler = new GetProductInfoListRequestHandler(_productRepositoryMock.Object, mockMapper.CreateMapper());

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.Count().Equals(productInfoList.Count);
    }

}
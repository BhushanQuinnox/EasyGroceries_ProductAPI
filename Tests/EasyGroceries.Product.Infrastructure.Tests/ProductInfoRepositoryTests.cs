using System;
using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using Dapper;
using EasyGroceries.Product.Domain;
using EasyGroceries.Product.Infrastructure.Contracts;
using EasyGroceries.Product.Infrastructure.Repositories;
using Microsoft.VisualBasic;
using Moq;

namespace EasyGroceries.Product.Infrastructure.Tests;

public class ProductInfoRepositoryTests
{
    private readonly Mock<IDapper> _dapperMock;

    public ProductInfoRepositoryTests()
    {
        _dapperMock = new Mock<IDapper>();
    }

    [Fact]
    public void GetAllProductsInfo_Should_Return_AllProducts()
    {
        // Arrange
        var fixture = new Fixture();
        var productList = fixture.Create<IReadOnlyList<ProductInfo>>();
        _dapperMock.Setup(x => x.GetAll<ProductInfo>(It.IsAny<string>(),
                 It.IsAny<ProductInfo>(), It.IsAny<System.Data.CommandType>())).Returns(productList.ToList());

        // Act
        ProductInfoRepository productInfoRepo = new ProductInfoRepository(_dapperMock.Object);
        var result = productInfoRepo.GetAllProductsInfo();

        // Assert
        Assert.Equal(productList.Count, result.Result.Count);
        Assert.Equal(productList.FirstOrDefault().ProductId, result.Result.FirstOrDefault().ProductId);
        Assert.Equal(productList.FirstOrDefault().Name, result.Result.FirstOrDefault().Name);
        Assert.Equal(productList.FirstOrDefault().Price, result.Result.FirstOrDefault().Price);
        Assert.Equal(productList.FirstOrDefault().Description, result.Result.FirstOrDefault().Description);
    }

    [Fact]
    public void GetProductInfoById_Should_Return_ProductOfSpecifiedId()
    {
        // Arrange
        var fixture = new Fixture();
        var productList = fixture.Create<IReadOnlyList<ProductInfo>>();
        int productIndex = 1;
        _dapperMock.Setup(x => x.Get<ProductInfo>(It.IsAny<string>(),
                 It.IsAny<DynamicParameters>(), It.IsAny<System.Data.CommandType>())).Returns(productList[productIndex]);

        // Act
        ProductInfoRepository productInfoRepo = new ProductInfoRepository(_dapperMock.Object);
        var result = productInfoRepo.GetProductInfoById(productIndex);

        // Assert
        Assert.Equal(productList[productIndex].ProductId, result.Result.ProductId);
        Assert.Equal(productList[productIndex].Name, result.Result.Name);
        Assert.Equal(productList[productIndex].Price, result.Result.Price);
        Assert.Equal(productList[productIndex].Description, result.Result.Description);
    }
}
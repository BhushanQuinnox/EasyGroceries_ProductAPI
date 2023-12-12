using System.Data;
using Dapper;
using EasyGroceries.Product.Application.Contracts.Infrastructure;
using EasyGroceries.Product.Domain;
using EasyGroceries.Product.Infrastructure.Contracts;

namespace EasyGroceries.Product.Infrastructure.Repositories
{
    public class ProductInfoRepository : IProductInfoRepository
    {
        private readonly IDapper _dapper;

        public ProductInfoRepository(IDapper dapper)
        {
            _dapper = dapper;
        }

        public async Task<IReadOnlyList<ProductInfo>> GetAllProductsInfo()
        {
            var query = "SELECT * FROM ProductInfo";
            var productList = await Task.FromResult(_dapper.GetAll<ProductInfo>(query, null, commandType: CommandType.Text));
            return productList;
        }

        public async Task<ProductInfo> GetProductInfoById(int id)
        {
            var query = "SELECT * FROM ProductInfo WHERE ProductId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int32, ParameterDirection.Input);
            var product = await Task.FromResult(_dapper.Get<ProductInfo>(query, parameters, CommandType.Text));
            return product;
        }
    }
}
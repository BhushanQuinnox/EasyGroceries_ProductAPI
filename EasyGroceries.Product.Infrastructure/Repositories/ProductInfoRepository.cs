using EasyGroceries.Product.Application.Contracts.Infrastructure;
using EasyGroceries.Product.Domain;
using Microsoft.Extensions.Configuration;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace EasyGroceries.Product.Infrastructure.Repositories
{
    public class ProductInfoRepository : IProductInfoRepository
    {
        private readonly string _connectionString;

        public ProductInfoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IReadOnlyList<ProductInfo>> GetAllProductsInfo()
        {
            var sql = "SELECT * FROM ProductInfo";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<ProductInfo>(sql);
                return result.ToList();
            }
        }

        public async Task<ProductInfo> GetProductInfoById(int id)
        {
            var sql = "SELECT * FROM ProductInfo WHERE ProductId = @id";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<ProductInfo>(sql, new { id });
                return result;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyGroceries.Product.Domain;

namespace EasyGroceries.Product.Application.Contracts.Infrastructure
{
    public interface IProductInfoRepository
    {
        Task<ProductInfo> GetProductInfoById(int id);
        Task<IReadOnlyList<ProductInfo>> GetAllProductsInfo();
    }
}

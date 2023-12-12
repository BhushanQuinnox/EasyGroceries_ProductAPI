using System;
using Dapper;
using System.Data;
using System.Data.Common;

namespace EasyGroceries.Product.Infrastructure.Contracts
{
    public interface IDapper : IDisposable
    {
        T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        List<T> GetAll<T>(string sp, T data, CommandType commandType = CommandType.StoredProcedure);
        int Execute<T>(string sp, List<T> data, CommandType commandType = CommandType.StoredProcedure);
        T Insert<T>(string sp, T data, CommandType commandType = CommandType.StoredProcedure);
        T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
    }
}
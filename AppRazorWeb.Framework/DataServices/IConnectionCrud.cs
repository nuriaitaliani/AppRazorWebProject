using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace AppRazorWeb.Framework.Dataservices
{
    public interface IConnectionCrud
    {

        Task<int> Delete(IDbConnection dbConnection, string query, DynamicParameters parameters, IDbTransaction dbTransaction);
        Task<IEnumerable<T>> Load<T>(IDbConnection dbConnection, string query, IDbTransaction dbTransaction);
        Task<IEnumerable<T>> Load<T, T1>(IDbConnection dbConnection, string query, T1 parameters, IDbTransaction dbTransaction);
        Task<IEnumerable<T>> Save<T, U>(IDbConnection dbConnection, string query, U parameters, IDbTransaction dbTransaction);
    }
}

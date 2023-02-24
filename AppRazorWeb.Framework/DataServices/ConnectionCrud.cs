using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AppRazorWeb.Framework.Dataservices;

namespace AppRazorWeb.Framework.DataServices
{
    public class ConnectionCrud : IConnectionCrud
    {
        public async Task<int> Delete(IDbConnection dbConnection, string query, DynamicParameters parameters, IDbTransaction dbTransaction)
        {
            return await dbConnection.ExecuteAsync(query, parameters, dbTransaction);
        }

        public async Task<IEnumerable<T>> Load<T>(IDbConnection dbConnection, string query, IDbTransaction dbTransaction)
        {
            return await dbConnection.QueryAsync<T>(query, dbTransaction);
        }

        public async Task<IEnumerable<T>> Load<T, U>(IDbConnection dbConnection, string query, U parameters, IDbTransaction dbTransaction)
        {
            return await dbConnection.QueryAsync<T>(query,parameters, dbTransaction);
        }

        public async Task<IEnumerable<T>> Save<T, U>(IDbConnection dbConnection, string query, U parameters, IDbTransaction dbTransaction)
        {
            return await dbConnection.QueryAsync<T>(query, parameters, dbTransaction);
        }
    }
}

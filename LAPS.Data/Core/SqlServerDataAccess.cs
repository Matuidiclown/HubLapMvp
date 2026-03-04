using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace LAPS.Data.Core
{
    public class SqlServerDataAccess : IDataAccess
    {
        private readonly IConfiguration _config;
        public SqlServerDataAccess(IConfiguration config) => _config = config;

        public async Task<IEnumerable<T>> LoadData<T>(string storedProcedure, object parameters, string connectionId = "DefaultConnection")
        {
            // El operador ?? "" evita el warning CS8600
            string connectionString = _config.GetConnectionString(connectionId) ?? "";

            using IDbConnection connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "DefaultConnection")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Logging;

namespace DataRepository.Repositories
{
    public class BrainWareRepository : IBrainWareRepository
    {
        private readonly string _connectionString;
       

        public BrainWareRepository(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string cannot be null or empty.");
            }
            _connectionString = connectionString;
          
        }

        public async Task<IEnumerable<T>> ExecuteStoredProcedure<T>(string storedProcedure, object parameters)
        {
            IEnumerable<T> results;

            using (var connection = new SqlConnection(_connectionString)) // Use injected connection (if available)
            {
                if (connection == null)
                {
                   
                    throw new InvalidOperationException("Connection is not available in DataRepository.");
                }

                try
                {
                    connection.Open();
                    results = await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                 
                    throw new DataException("An error occurred while executing the stored procedure.", ex);
                }
            }

            return results;
        }
    }
}

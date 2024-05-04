using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;




namespace DataRepository.Repositories
{
    public class BrainWareRepository : IBrainWareRepository
    {
        private readonly string _connectionString; // Replace with your actual connection string       


        public BrainWareRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException("null db connection string");
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
                    // Log the exception details (consider using a logger)
                    Console.WriteLine($"Error executing stored procedure: {ex.Message}");

                    // Re-throw or handle the exception as needed
                    throw new DataException("An error occurred while executing the stored procedure.", ex);
                }
            }

            return results;
        }

    }

}
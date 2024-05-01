using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<T>> GetOrders<T>(string sql, object parameters = null)
        {
            IEnumerable<T> orders;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                orders = await connection.QueryAsync<T>(sql, parameters);
            }

            return orders;
        }

    }
}
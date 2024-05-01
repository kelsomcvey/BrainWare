namespace Api.Infrastructure
{
    using System.Data.Common;
    using System.Data.SqlClient;

    public class Database
    {
        private readonly SqlConnection _connection;

        public Database()
        {
             var connectionString = "Data Source=LOCALHOST;Initial Catalog=ProjectDB;Integrated Security=SSPI";
           // var mdf = @"C:\temp\BrainWare\BrainWare\Api\data\BrainWare.mdf";
           // var connectionString = $"Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=BrainWAre;Integrated Security=SSPI;AttachDBFilename={mdf}";

            _connection = new SqlConnection(connectionString);

            _connection.Open();
        }

        public async Task<DbDataReader> ExecuteReader(string query)
        {
           

            var sqlQuery = new SqlCommand(query, _connection);

            return await sqlQuery.ExecuteReaderAsync();
        }

        public int ExecuteNonQuery(string query)
        {
            var sqlQuery = new SqlCommand(query, _connection);

            return sqlQuery.ExecuteNonQuery();
        }

    }
}
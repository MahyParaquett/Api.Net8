using Npgsql;
using Repository.Contracts;
using System.Data;
using System.Data.SqlClient;

namespace Repository.Connection
{
    public class DefaultSqlConnectionFactory : IConnectionFactory
    {
        public IDbConnection Connection()
        {
            return new NpgsqlConnection("Server=localhost;Port=5432;Database=HeroDB1;User Id=postgres;Password=123456;"); ;
         }
    }
}

using System.Data;
using Microsoft.Data.SqlClient;
using ProjectUser.Common.Interface;
using Microsoft.Extensions.Configuration;

namespace ProjectUser.Common.Helpers
{
    public class UserDbCommon : IUserDbCommon
    {
        private readonly string? _connectionString;

        public UserDbCommon(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("UserDbContext");

            if (_connectionString == null) { throw new Exception("SQL Connection Fail"); }
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
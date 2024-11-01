using System;
using Npgsql;

namespace POS.Helpers
{
    public static class ConnectionHelper
    {
        public static string BuildConnectionString()
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = Environment.GetEnvironmentVariable("DB_HOST"),
                Port = int.Parse(Environment.GetEnvironmentVariable("DB_PORT")),
                Database = Environment.GetEnvironmentVariable("DB_NAME"),
                Username = Environment.GetEnvironmentVariable("DB_USER"),
                Password = Environment.GetEnvironmentVariable("DB_PASS")
            };
            return builder.ConnectionString;
        }
    }
}

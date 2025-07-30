using System;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace GorevTakipAPI.Infrastructure.Persistence
{
    public static class DatabaseInitializer
    {
        public static void Initialize(IConfiguration config)
        {

            using var adminConn = new NpgsqlConnection(config.GetConnectionString("Admin"));
            adminConn.Open();
            using var cmd = adminConn.CreateCommand();
            cmd.CommandText = "SELECT 1 FROM pg_database WHERE datname = 'TaskTracking'";
            var exists = cmd.ExecuteScalar() != null;
            if (!exists)
            {
                cmd.CommandText = "CREATE DATABASE \"TaskTracking\"";
                cmd.ExecuteNonQuery();
            }
            adminConn.Close();
            using var userConn = new NpgsqlConnection(config.GetConnectionString("Default"));
            userConn.Open();
            using var cmd2 = userConn.CreateCommand();
            cmd2.CommandText = @"
                CREATE TABLE IF NOT EXISTS Tasks (
                    Id SERIAL PRIMARY KEY,
                    Title VARCHAR(200) NOT NULL,
                    Description TEXT,
                    iscompleted BOOLEAN NOT NULL DEFAULT FALSE
                );";
            cmd2.ExecuteNonQuery();
            userConn.Close();
        }
    }
}

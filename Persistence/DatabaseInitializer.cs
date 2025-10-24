using System;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace GorevTakipAPI.Infrastructure.Persistence
{
    public static class DatabaseInitializer
    {
        public static void Initialize(IConfiguration config)
        {
            var adminConnectionString = config.GetConnectionString("Admin");
            var defaultConnectionString = config.GetConnectionString("Default");

            if (string.IsNullOrWhiteSpace(defaultConnectionString))
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(adminConnectionString))
            {
                using var adminConn = new NpgsqlConnection(adminConnectionString);
                adminConn.Open();
                using var cmd = adminConn.CreateCommand();
                cmd.CommandText = "SELECT 1 FROM pg_database WHERE datname = 'TaskTracking'";
                var exists = cmd.ExecuteScalar() != null;
                if (!exists)
                {
                    cmd.CommandText = "CREATE DATABASE \"TaskTracking\"";
                    cmd.ExecuteNonQuery();
                }
            }

            using var userConn = new NpgsqlConnection(defaultConnectionString);
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

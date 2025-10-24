using System;
using System.Collections.Generic;
using GorevTakipAPI.Domain.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace GorevTakipAPI.Infrastructure.Persistence.Repositories
{
    public class TaskRepository
    {
        private readonly string _connectionString;

        public TaskRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public IEnumerable<TodoTask> GetAll()
        {
            var list = new List<TodoTask>();
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Title, Description, iscompleted FROM Tasks ORDER BY Id;";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new TodoTask
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                    IsCompleted = reader.GetBoolean(3)
                });
            }

            return list;
        }

        public void Add(TodoTask task)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Tasks (Title, Description, iscompleted) VALUES (@title, @desc, false) RETURNING Id;";
            cmd.Parameters.AddWithValue("title", task.Title);
            cmd.Parameters.AddWithValue("desc", task.Description ?? string.Empty);

            task.Id = (int)cmd.ExecuteScalar()!;
        }

        public void Update(TodoTask task)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE Tasks SET Title = @title, Description = @desc WHERE Id = @id;";
            cmd.Parameters.AddWithValue("title", task.Title);
            cmd.Parameters.AddWithValue("desc", string.IsNullOrWhiteSpace(task.Description)
                ? (object)DBNull.Value
                : task.Description);
            cmd.Parameters.AddWithValue("id", task.Id);
            cmd.ExecuteNonQuery();
        }

        public void MarkCompleted(int id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE Tasks SET iscompleted = true WHERE Id = @id;";
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Tasks WHERE Id = @id;";
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }
    }
}

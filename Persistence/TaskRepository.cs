using System.Collections.Generic;
using Npgsql;
using GorevTakipAPI.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace GorevTakipAPI.Infrastructure.Persistence
{
    public class TaskRepository
    {
        private readonly string _conn;
        public TaskRepository(IConfiguration config) =>
            _conn = config.GetConnectionString("Default");

        public IEnumerable<TodoTask> GetAll()
        {
            var list = new List<TodoTask>();
            using var conn = new NpgsqlConnection(_conn);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Title, Description, iscompleted FROM Tasks ORDER BY Id;";
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(new TodoTask
                {
                    Id = rdr.GetInt32(0),
                    Title = rdr.GetString(1),
                    Description = rdr.IsDBNull(2) ? null : rdr.GetString(2),
                    IsCompleted = rdr.GetBoolean(3)
                });
            }
            return list;
        }

        public void Add(TodoTask t)
        {
            using var conn = new NpgsqlConnection(_conn);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Tasks (Title, Description, iscompleted) VALUES (@t, @d, false) RETURNING Id;";
            cmd.Parameters.AddWithValue("t", t.Title);
            cmd.Parameters.AddWithValue("d", t.Description ?? string.Empty);
            t.Id = (int)cmd.ExecuteScalar()!;
        }

        public void MarkCompleted(int id)
        {
            using var conn = new NpgsqlConnection(_conn);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE Tasks SET iscompleted = true WHERE Id = @i;";
            cmd.Parameters.AddWithValue("i", id);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new NpgsqlConnection(_conn);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Tasks WHERE Id = @i;";
            cmd.Parameters.AddWithValue("i", id);
            cmd.ExecuteNonQuery();
        }
    }
}

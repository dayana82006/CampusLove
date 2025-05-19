using System.Collections.Generic;
using System.Data;
using Npgsql;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Infrastructure.Repositories
{
    public class PgsqlStatesRepository : IStatesRepository
    {
        private readonly string _connectionString;

        public PgsqlStatesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Insert(States state)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            using var cmd = new NpgsqlCommand("INSERT INTO states (id_country, state_name) VALUES (@id_country, @state_name)", conn);
            cmd.Parameters.AddWithValue("id_country", state.id_country);
            cmd.Parameters.AddWithValue("state_name", state.state_name);
            cmd.ExecuteNonQuery();
        }

        public States? GetById(int id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            using var cmd = new NpgsqlCommand("SELECT id_state, id_country, state_name FROM states WHERE id_state = @id", conn);
            cmd.Parameters.AddWithValue("id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new States
                {
                    id_state = reader.GetInt32(0),
                    id_country = reader.GetInt32(1),
                    state_name = reader.GetString(2)
                };
            }
            return null;
        }

        public IEnumerable<States> GetAll()
        {
            var list = new List<States>();
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            using var cmd = new NpgsqlCommand("SELECT id_state, id_country, state_name FROM states", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new States
                {
                    id_state = reader.GetInt32(0),
                    id_country = reader.GetInt32(1),
                    state_name = reader.GetString(2)
                });
            }
            return list;
        }

        public void Update(States state)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            using var cmd = new NpgsqlCommand("UPDATE states SET id_country = @id_country, state_name = @state_name WHERE id_state = @id_state", conn);
            cmd.Parameters.AddWithValue("id_country", state.id_country);
            cmd.Parameters.AddWithValue("state_name", state.state_name);
            cmd.Parameters.AddWithValue("id_state", state.id_state);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            using var cmd = new NpgsqlCommand("DELETE FROM states WHERE id_state = @id", conn);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }
    }
}

using System.Collections.Generic;
using Npgsql;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Domain.Interfaces 
{
    public class PgsqlCareersRepository : ICareersRepository
    {
        private readonly string _connectionString;

        public PgsqlCareersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Careers> GetAll()
        {
            var list = new List<Careers>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_career, career_name FROM careers", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Careers
                {
                    id_career = reader.GetInt32(0),
                    career_name = reader.GetString(1)
                });
            }

            return list;
        }

        public Careers GetById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_career, career_name FROM careers WHERE id_career = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Careers
                {
                    id_career = reader.GetInt32(0),
                    career_name = reader.GetString(1)
                };
            }

            return null;
        }
        public void Create(Careers career)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("INSERT INTO careers (career_name) VALUES (@career_name)", connection);
            command.Parameters.AddWithValue("@career_name", career.career_name);
            command.ExecuteNonQuery();
        }
        public void Update(Careers career)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("UPDATE careers SET career_name = @career_name WHERE id_career = @id", connection);
            command.Parameters.AddWithValue("@career_name", career.career_name);
            command.Parameters.AddWithValue("@id", career.id_career);
            command.ExecuteNonQuery();
        }
        public void Delete(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("DELETE FROM careers WHERE id_career = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}

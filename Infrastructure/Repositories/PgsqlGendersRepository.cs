using System.Collections.Generic;
using System.Data;
using Npgsql;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Domain.Interfaces 
{
    public class PgsqlGendersRepository : IGendersRepository
    {
        private readonly string _connectionString;

        public PgsqlGendersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Genders> GetAll()
        {
            var list = new List<Genders>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_gender, genre_name FROM genders", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Genders
                {
                    id_gender = reader.GetInt32(0),
                    genre_name = reader.GetString(1)
                });
            }

            return list;
        }

        public Genders GetById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_gender, genre_name FROM genders WHERE id_gender = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Genders
                {
                    id_gender = reader.GetInt32(0),
                    genre_name = reader.GetString(1)
                };
            }

            return null;
        }

        public void Create(Genders entity)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("INSERT INTO Genders (genre_name) VALUES (@genre_name)", connection);
            command.Parameters.AddWithValue("@genre_name", entity.genre_name);
            command.ExecuteNonQuery();
        }

        public void Update(Genders entity)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("UPDATE Genders SET genre_name = @genre_name WHERE id_}gender = @id", connection);
            command.Parameters.AddWithValue("@genre_name", entity.genre_name);
            command.Parameters.AddWithValue("@id", entity.id_gender);
            command.ExecuteNonQuery();  
        }

        public void Delete(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("DELETE FROM Genders WHERE id_gender = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}

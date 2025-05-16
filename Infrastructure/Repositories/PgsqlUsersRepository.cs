using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;

namespace CampusLove.Infrastructure.Repositories
{
    public class PgsqlUsersRepository : IUsersRepository
    {
        private readonly string _connectionString;

        public PgsqlUsersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private Users MapUser(NpgsqlDataReader reader)
        {
            return new Users
            {
                id_user = reader.GetInt32(reader.GetOrdinal("id_user")),
                first_name = reader.GetString(reader.GetOrdinal("first_name")),
                last_name = reader.GetString(reader.GetOrdinal("last_name")),
                email = reader.GetString(reader.GetOrdinal("email")),
                password = reader.GetString(reader.GetOrdinal("password")),
                birth_date = reader.GetDateTime(reader.GetOrdinal("birth_date")), // Cambiado de age a birth_date
                id_gender = reader.GetInt32(reader.GetOrdinal("id_gender")),
                id_career = reader.GetInt32(reader.GetOrdinal("id_career")),
                id_address = reader.GetInt32(reader.GetOrdinal("id_address")),
                profile_phrase = reader.GetString(reader.GetOrdinal("profile_phrase")),
            };
        }

        public Users? GetByEmail(string email)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var query = "SELECT * FROM Users WHERE email = @Email";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", email);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapUser(reader);
            }
            return null;
        }

        public Users? GetByUser(string username)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var query = "SELECT * FROM Users WHERE first_name = @username"; // O reemplaza por usuario_name si tienes ese campo
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapUser(reader);
            }
            return null;
        }

        public Users? GetById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var query = "SELECT * FROM Users WHERE id_user = @id_user";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id_user", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapUser(reader);
            }
            return null;
        }

        public IEnumerable<Users> GetAll()
        {
            var users = new List<Users>();
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var query = "SELECT * FROM Users";
            using var command = new NpgsqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                users.Add(MapUser(reader));
            }
            return users;
        }

        public void Create(Users user)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var query = @"INSERT INTO Users (first_name, last_name, email, password, birth_date, id_gender, id_career, id_address, profile_phrase)
                          VALUES (@first_name, @last_name , @email , @password, @birth_date, @id_gender, @id_career, @id_address, @profile_phrase)";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@first_name", user.first_name);
            command.Parameters.AddWithValue("@last_name", user.last_name);
            command.Parameters.AddWithValue("@email", user.email);
            command.Parameters.AddWithValue("@password", user.password);
            command.Parameters.AddWithValue("@birth_date", user.birth_date);
            command.Parameters.AddWithValue("@id_gender", user.id_gender);
            command.Parameters.AddWithValue("@id_career", user.id_career);
            command.Parameters.AddWithValue("@id_address", user.id_address);
            command.Parameters.AddWithValue("@profile_phrase", user.profile_phrase);
            command.ExecuteNonQuery();
        }

        public void Update(Users user)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var query = @"UPDATE Users SET first_name = @first_name, last_name = @last_name , email = @email , password = @password,
                          birth_date = @birth_date, id_gender = @id_gender, id_career = @id_career, id_address = @id_address,
                          profile_phrase = @profile_phrase WHERE id_user = @id_user";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@first_name", user.first_name);
            command.Parameters.AddWithValue("@last_name", user.last_name);
            command.Parameters.AddWithValue("@email", user.email);
            command.Parameters.AddWithValue("@password", user.password);
            command.Parameters.AddWithValue("@birth_date", user.birth_date);
            command.Parameters.AddWithValue("@id_gender", user.id_gender);
            command.Parameters.AddWithValue("@id_career", user.id_career);
            command.Parameters.AddWithValue("@id_address", user.id_address);
            command.Parameters.AddWithValue("@profile_phrase", user.profile_phrase);
            command.Parameters.AddWithValue("@id_user", user.id_user);
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var query = "DELETE FROM Users WHERE id_user = @id_user";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id_user", id);
            command.ExecuteNonQuery();
        }
    }
}

using System.Collections.Generic;
using Npgsql;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Infrastructure.Repositories
{
    public class PgsqlUsersInterestsRepository : IUsersInterestsRepository
    {
        private readonly string _connectionString;

        public PgsqlUsersInterestsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUserInterest(int id_user, int id_interest)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand(
                "INSERT INTO UsersInterests (id_user, id_interest) VALUES (@id_user, @id_interest)", connection);
            command.Parameters.AddWithValue("@id_user", id_user);
            command.Parameters.AddWithValue("@id_interest", id_interest);

            command.ExecuteNonQuery();
        }

        public void AddUserInterests(int id_user, IEnumerable<int> interests)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            foreach (var interestId in interests)
            {
                var command = new NpgsqlCommand(
                    "INSERT INTO UsersInterests (id_user, id_interest) VALUES (@id_user, @id_interest)", connection);
                command.Parameters.AddWithValue("@id_user", id_user);
                command.Parameters.AddWithValue("@id_interest", interestId);
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<UsersInterests> GetByUserId(int userId)
        {
            var list = new List<UsersInterests>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand(
                "SELECT id_user, id_interest FROM UsersInterests WHERE id_user = @userId", connection);
            command.Parameters.AddWithValue("@userId", userId);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new UsersInterests
                {
                    id_user = reader.GetInt32(0),
                    id_interest = reader.GetInt32(1)
                });
            }

            return list;
        }
    }
}

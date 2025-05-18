using System;
using Npgsql;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Infrastructure.Repositories
{
    public class PgsqlMatchesRepository : IMatchesRepository
    {
        private readonly string _connectionString;

        public PgsqlMatchesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Insert(Matches match)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            const string query = @"
                INSERT INTO Matches (id_user1, id_user2, match_date)
                VALUES (@user1, @user2, @date);
            ";

            using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@user1", match.id_user1);
            cmd.Parameters.AddWithValue("@user2", match.id_user2);
            cmd.Parameters.AddWithValue("@date", match.match_date);

            cmd.ExecuteNonQuery();
        }

                public IEnumerable<Matches> GetAllMatches()
        {
            var matches = new List<Matches>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            const string query = "SELECT id_user1, id_user2, match_date FROM Matches";

            using var cmd = new NpgsqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                matches.Add(new Matches
                {
                    id_user1 = reader.GetInt32(0),
                    id_user2 = reader.GetInt32(1),
                    match_date = reader.GetDateTime(2)
                });
            }

            return matches;
        }

        public bool MatchExists(int userId1, int userId2)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            const string query = @"
                SELECT COUNT(*) 
                FROM Matches 
                WHERE (id_user1 = @user1 AND id_user2 = @user2)
                   OR (id_user1 = @user2 AND id_user2 = @user1);
            ";

            using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@user1", userId1);
            cmd.Parameters.AddWithValue("@user2", userId2);

            var count = (long)cmd.ExecuteScalar();
            return count > 0;
        }
    }
}

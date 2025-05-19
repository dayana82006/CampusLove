using System;
using System.Collections.Generic;
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

            const string query = "SELECT id_match, id_user1, id_user2, match_date FROM Matches";

            using var cmd = new NpgsqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                matches.Add(new Matches
                {
                    id_match = reader.GetInt32(0),
                    id_user1 = reader.GetInt32(1),
                    id_user2 = reader.GetInt32(2),
                    match_date = reader.GetDateTime(3)
                });
            }

            return matches;
        }

        public bool MatchExists(int userId1, int userId2)
        {
            // Asegurar que el orden de los IDs sea consistente
            var (user1, user2) = userId1 < userId2 ? (userId1, userId2) : (userId2, userId1);

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            const string query = @"
                SELECT COUNT(*) 
                FROM Matches 
                WHERE id_user1 = @user1 AND id_user2 = @user2;
            ";

            using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@user1", user1);
            cmd.Parameters.AddWithValue("@user2", user2);

            var count = (long)cmd.ExecuteScalar();
            return count > 0;
        }

        public int GetMatchId(int userId1, int userId2)
        {
            // Asegurar que el orden de los IDs sea consistente
            var (user1, user2) = userId1 < userId2 ? (userId1, userId2) : (userId2, userId1);

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            const string query = @"
                SELECT id_match 
                FROM Matches 
                WHERE id_user1 = @user1 AND id_user2 = @user2;
            ";

            using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@user1", user1);
            cmd.Parameters.AddWithValue("@user2", user2);

            var result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : -1;
        }

        public void Delete(int matchId)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            const string query = "DELETE FROM Matches WHERE id_match = @matchId";

            using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@matchId", matchId);

            cmd.ExecuteNonQuery();
        }

        public void UpdateMatchDate(int matchId, DateTime newDate)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            const string query = "UPDATE Matches SET match_date = @newDate WHERE id_match = @matchId";

            using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@matchId", matchId);
            cmd.Parameters.AddWithValue("@newDate", newDate);

            cmd.ExecuteNonQuery();
        }
    }
}
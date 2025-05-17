using System;
using System.Data;
using Npgsql;
using CampusLove.Domain.Interfaces;
using CampusLove.Domain.Entities;

namespace CampusLove.Infrastructure.Repositories
{
    public class PgsqlInteractionCreditsRepository : IInteractionCreditsRepository
    {
        private readonly string _connectionString;

        public PgsqlInteractionCreditsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int GetCreditsByUserId(int userId)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            const string query = @"
                SELECT available_credits 
                FROM InteractionCredits 
                WHERE id_user = @userId;
            ";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", userId);

            var result = command.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public void UpdateCredits(int userId, int creditChange)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            const string query = @"
                UPDATE InteractionCredits
                SET available_credits = available_credits + @creditChange,
                    last_update_date = CURRENT_DATE
                WHERE id_user = @userId;
            ";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@creditChange", creditChange);
            command.Parameters.AddWithValue("@userId", userId);

            command.ExecuteNonQuery();
        }

        public void SetInitialCredits(int userId, int initialCredits)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            const string query = @"
                INSERT INTO InteractionCredits (id_user, available_credits, last_update_date)
                VALUES (@userId, @initialCredits, CURRENT_DATE)
                ON CONFLICT (id_user) DO UPDATE 
                SET available_credits = EXCLUDED.available_credits,
                    last_update_date = CURRENT_DATE;
            ";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@initialCredits", initialCredits);

            command.ExecuteNonQuery();
        }

        public InteractionCredits GetByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public void Add(InteractionCredits credits)
        {
            throw new NotImplementedException();
        }

        public void Update(InteractionCredits credits)
        {
            throw new NotImplementedException();
        }
    }
}

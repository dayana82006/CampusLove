using System;
using System.Data;
using Npgsql;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Infrastructure.Repositories
{
    public class PgsqlInteractionCreditsRepository : IInteractionsCreditsRepository
    {
        private readonly string _connectionString;

        public PgsqlInteractionCreditsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int GetCreditsByUserId(int userId)
        {
            return GetByUserId(userId)?.available_credits ?? 0;
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
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            const string query = @"
                SELECT id_user, available_credits, last_update_date
                FROM InteractionCredits
                WHERE id_user = @userId;
            ";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", userId);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new InteractionCredits
                {
                    id_user = reader.GetInt32(0),
                    available_credits = reader.GetInt32(1),
                    last_update_date = reader.GetDateTime(2)
                };
            }

            return null;
        }

        public void Add(InteractionCredits credits)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            const string query = @"
                INSERT INTO InteractionCredits (id_user, available_credits, last_update_date)
                VALUES (@userId, @credits, @date);
            ";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", credits.id_user);
            command.Parameters.AddWithValue("@credits", credits.available_credits);
            command.Parameters.AddWithValue("@date", credits.last_update_date);

            command.ExecuteNonQuery();
        }

        public void Update(InteractionCredits credits)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            const string query = @"
                UPDATE InteractionCredits
                SET available_credits = @credits,
                    last_update_date = @date
                WHERE id_user = @userId;
            ";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@credits", credits.available_credits);
            command.Parameters.AddWithValue("@date", credits.last_update_date);
            command.Parameters.AddWithValue("@userId", credits.id_user);

            command.ExecuteNonQuery();
        }
    }
}

using System;
using System.Collections.Generic;
using Npgsql;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Infrastructure.Repositories
{
    public class PgsqlInteractionsRepository : IInteractionsRepository
    {
        private readonly string _connectionString;

        public PgsqlInteractionsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Interactions interaction)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            var sql = @"INSERT INTO interactions (id_user_origin, id_user_target, interaction_type, interaction_date) 
                        VALUES (@id_user_origin, @id_user_target, @interaction_type, @interaction_date)";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id_user_origin", interaction.id_user_origin);
            cmd.Parameters.AddWithValue("id_user_target", interaction.id_user_target);
            cmd.Parameters.AddWithValue("interaction_type", interaction.interaction_type);
            cmd.Parameters.AddWithValue("interaction_date", interaction.interaction_date);

            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Interactions> GetAll()
        {
            var list = new List<Interactions>();
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            var sql = "SELECT id_user_origin, id_user_target, interaction_type, interaction_date FROM interactions";

            using var cmd = new NpgsqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Interactions
                {
                    id_user_origin = reader.GetInt32(0),
                    id_user_target = reader.GetInt32(1),
                    interaction_type = reader.GetString(2),
                    interaction_date = reader.GetDateTime(3)
                });
            }

            return list;
        }

        public IEnumerable<Interactions> GetByUserId(int userId)
        {
            var list = new List<Interactions>();
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            var sql = @"SELECT id_user_origin, id_user_target, interaction_type, interaction_date
                        FROM interactions
                        WHERE id_user_origin = @userId";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("userId", userId);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Interactions
                {
                    id_user_origin = reader.GetInt32(0),
                    id_user_target = reader.GetInt32(1),
                    interaction_type = reader.GetString(2),
                    interaction_date = reader.GetDateTime(3)
                });
            }

            return list;
        }
        public void Update(Interactions interaction)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            var sql = @"UPDATE interactions
                        SET interaction_type = @interaction_type,
                            interaction_date = @interaction_date
                        WHERE id_user_origin = @id_user_origin
                        AND id_user_target = @id_user_target";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("interaction_type", interaction.interaction_type);
            cmd.Parameters.AddWithValue("interaction_date", interaction.interaction_date);
            cmd.Parameters.AddWithValue("id_user_origin", interaction.id_user_origin);
            cmd.Parameters.AddWithValue("id_user_target", interaction.id_user_target);

            cmd.ExecuteNonQuery();
        }

        public IEnumerable<Interactions> GetByDate(DateTime date)
        {
            var list = new List<Interactions>();
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            var sql = @"SELECT id_user_origin, id_user_target, interaction_type, interaction_date
                        FROM interactions
                        WHERE interaction_date::date = @date";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("date", date.Date);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Interactions
                {
                    id_user_origin = reader.GetInt32(0),
                    id_user_target = reader.GetInt32(1),
                    interaction_type = reader.GetString(2),
                    interaction_date = reader.GetDateTime(3)
                });
            }

            return list;
        }

        public bool HasInteractedWith(int userId, int targetUserId)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            var sql = @"SELECT EXISTS(
                            SELECT 1 FROM interactions
                            WHERE id_user_origin = @userId AND id_user_target = @targetUserId
                        )";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("userId", userId);
            cmd.Parameters.AddWithValue("targetUserId", targetUserId);

            return (bool)cmd.ExecuteScalar();
        }

        public IEnumerable<Interactions> GetByUserIdAndDate(int userId, DateTime date)
        {
            var list = new List<Interactions>();
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            var sql = @"SELECT id_user_origin, id_user_target, interaction_type, interaction_date
                        FROM interactions
                        WHERE id_user_origin = @userId AND interaction_date::date = @date";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("userId", userId);
            cmd.Parameters.AddWithValue("date", date.Date);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Interactions
                {
                    id_user_origin = reader.GetInt32(0),
                    id_user_target = reader.GetInt32(1),
                    interaction_type = reader.GetString(2),
                    interaction_date = reader.GetDateTime(3)
                });
            }

            return list;
        }

        // IMPLEMENTACIÓN REQUERIDA: Exists
        public bool Exists(int userId, int targetUserId)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            var sql = @"SELECT COUNT(*) FROM interactions
                        WHERE id_user_origin = @userId AND id_user_target = @targetUserId";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("userId", userId);
            cmd.Parameters.AddWithValue("targetUserId", targetUserId);

            var count = (long)cmd.ExecuteScalar();
            return count > 0;
        }

        // IMPLEMENTACIÓN REQUERIDA: GetInteraction
        public Interactions? GetInteraction(int originId, int targetId)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            var sql = @"SELECT id_user_origin, id_user_target, interaction_type, interaction_date
                        FROM interactions
                        WHERE id_user_origin = @originId AND id_user_target = @targetId
                        LIMIT 1";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("originId", originId);
            cmd.Parameters.AddWithValue("targetId", targetId);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Interactions
                {
                    id_user_origin = reader.GetInt32(0),
                    id_user_target = reader.GetInt32(1),
                    interaction_type = reader.GetString(2),
                    interaction_date = reader.GetDateTime(3)
                };
            }

            return null;
        }
    }
}

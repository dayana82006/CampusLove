using System;
using System.Collections.Generic;
using System.Linq;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;
using Npgsql;

namespace CampusLove.Infrastructure.Repositories
{
    public class PgsqlUserStatisticsRepository : IUserStatisticsRepository
    {
        private readonly string _connectionString;

        public PgsqlUserStatisticsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public UserStatistics GetByUserId(int userId)
        {
            UserStatistics userStats = null;

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = @"
                        SELECT id_user, received_likes, received_dislikes, 
                               sent_likes, sent_dislikes, total_matches, last_update 
                        FROM UserStatistics 
                        WHERE id_user = @userId";
                    
                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userStats = new UserStatistics
                                {
                                    id_user = reader.GetInt32(0),
                                    received_likes = reader.GetInt32(1),
                                    received_dislikes = reader.GetInt32(2),
                                    sent_likes = reader.GetInt32(3),
                                    sent_dislikes = reader.GetInt32(4),
                                    total_matches = reader.GetInt32(5),
                                    last_update = reader.GetDateTime(6)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener estadísticas del usuario: {ex.Message}");
            }

            return userStats;
        }

        public void Add(UserStatistics stats)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = @"
                        INSERT INTO UserStatistics 
                        (id_user, received_likes, received_dislikes, sent_likes, sent_dislikes, total_matches, last_update)
                        VALUES 
                        (@userId, @receivedLikes, @receivedDislikes, @sentLikes, @sentDislikes, @totalMatches, @lastUpdate)";
                    
                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@userId", stats.id_user);
                        command.Parameters.AddWithValue("@receivedLikes", stats.received_likes);
                        command.Parameters.AddWithValue("@receivedDislikes", stats.received_dislikes);
                        command.Parameters.AddWithValue("@sentLikes", stats.sent_likes);
                        command.Parameters.AddWithValue("@sentDislikes", stats.sent_dislikes);
                        command.Parameters.AddWithValue("@totalMatches", stats.total_matches);
                        command.Parameters.AddWithValue("@lastUpdate", stats.last_update);
                        
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al agregar estadísticas del usuario: {ex.Message}");
            }
        }

        public void Update(UserStatistics stats)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = @"
                        UPDATE UserStatistics 
                        SET received_likes = @receivedLikes,
                            received_dislikes = @receivedDislikes,
                            sent_likes = @sentLikes,
                            sent_dislikes = @sentDislikes,
                            total_matches = @totalMatches,
                            last_update = @lastUpdate
                        WHERE id_user = @userId";
                    
                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@userId", stats.id_user);
                        command.Parameters.AddWithValue("@receivedLikes", stats.received_likes);
                        command.Parameters.AddWithValue("@receivedDislikes", stats.received_dislikes);
                        command.Parameters.AddWithValue("@sentLikes", stats.sent_likes);
                        command.Parameters.AddWithValue("@sentDislikes", stats.sent_dislikes);
                        command.Parameters.AddWithValue("@totalMatches", stats.total_matches);
                        command.Parameters.AddWithValue("@lastUpdate", stats.last_update);
                        
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al actualizar estadísticas del usuario: {ex.Message}");
            }
        }

        public IEnumerable<UserStatistics> GetAll()
        {
            var statistics = new List<UserStatistics>();
            
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = @"
                        SELECT id_user, received_likes, received_dislikes, 
                               sent_likes, sent_dislikes, total_matches, last_update
                        FROM UserStatistics";
                    
                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                statistics.Add(new UserStatistics
                                {
                                    id_user = reader.GetInt32(0),
                                    received_likes = reader.GetInt32(1),
                                    received_dislikes = reader.GetInt32(2),
                                    sent_likes = reader.GetInt32(3),
                                    sent_dislikes = reader.GetInt32(4),
                                    total_matches = reader.GetInt32(5),
                                    last_update = reader.GetDateTime(6)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener todas las estadísticas: {ex.Message}");
            }
            
            return statistics;
        }

        // Método para obtener usuarios con más likes recibidos
        public List<UserStatisticsWithUserInfo> GetTopUsersByLikes(int limit = 5)
        {
            List<UserStatisticsWithUserInfo> topUsers = new List<UserStatisticsWithUserInfo>();

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = @"
                        SELECT us.id_user, us.received_likes, us.received_dislikes, 
                               us.sent_likes, us.sent_dislikes, us.total_matches, 
                               u.first_name, u.last_name, u.birth_date, u.profile_phrase
                        FROM UserStatistics us
                        JOIN Users u ON us.id_user = u.id_user
                        ORDER BY us.received_likes DESC
                        LIMIT @limit";
                    
                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@limit", limit);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                topUsers.Add(new UserStatisticsWithUserInfo
                                {
                                    id_user = reader.GetInt32(0),
                                    received_likes = reader.GetInt32(1),
                                    received_dislikes = reader.GetInt32(2),
                                    sent_likes = reader.GetInt32(3),
                                    sent_dislikes = reader.GetInt32(4),
                                    total_matches = reader.GetInt32(5),
                                    first_name = reader.GetString(6),
                                    last_name = reader.GetString(7),
                                    birth_date = reader.GetDateTime(8),
                                    profile_phrase = reader.GetString(9)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener usuarios con más likes: {ex.Message}");
            }

            return topUsers;
        }

        // Método para obtener usuarios con más matches
        public List<UserStatisticsWithUserInfo> GetTopUsersByMatches(int limit = 5)
        {
            List<UserStatisticsWithUserInfo> topUsers = new List<UserStatisticsWithUserInfo>();

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = @"
                        SELECT us.id_user, us.received_likes, us.received_dislikes, 
                               us.sent_likes, us.sent_dislikes, us.total_matches, 
                               u.first_name, u.last_name, u.birth_date, u.profile_phrase
                        FROM UserStatistics us
                        JOIN Users u ON us.id_user = u.id_user
                        ORDER BY us.total_matches DESC
                        LIMIT @limit";
                    
                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@limit", limit);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                topUsers.Add(new UserStatisticsWithUserInfo
                                {
                                    id_user = reader.GetInt32(0),
                                    received_likes = reader.GetInt32(1),
                                    received_dislikes = reader.GetInt32(2),
                                    sent_likes = reader.GetInt32(3),
                                    sent_dislikes = reader.GetInt32(4),
                                    total_matches = reader.GetInt32(5),
                                    first_name = reader.GetString(6),
                                    last_name = reader.GetString(7),
                                    birth_date = reader.GetDateTime(8),
                                    profile_phrase = reader.GetString(9)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener usuarios con más matches: {ex.Message}");
            }

            return topUsers;
        }

        // Método para obtener usuarios más activos 
        public List<UserStatisticsWithUserInfo> GetMostActiveUsers(int limit = 5)
        {
            List<UserStatisticsWithUserInfo> topUsers = new List<UserStatisticsWithUserInfo>();

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = @"
                        SELECT us.id_user, us.received_likes, us.received_dislikes, 
                               us.sent_likes, us.sent_dislikes, us.total_matches, 
                               u.first_name, u.last_name, u.birth_date, u.profile_phrase
                        FROM UserStatistics us
                        JOIN Users u ON us.id_user = u.id_user
                        ORDER BY us.sent_likes DESC
                        LIMIT @limit";
                    
                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@limit", limit);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                topUsers.Add(new UserStatisticsWithUserInfo
                                {
                                    id_user = reader.GetInt32(0),
                                    received_likes = reader.GetInt32(1),
                                    received_dislikes = reader.GetInt32(2),
                                    sent_likes = reader.GetInt32(3),
                                    sent_dislikes = reader.GetInt32(4),
                                    total_matches = reader.GetInt32(5),
                                    first_name = reader.GetString(6),
                                    last_name = reader.GetString(7),
                                    birth_date = reader.GetDateTime(8),
                                    profile_phrase = reader.GetString(9)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener usuarios más activos: {ex.Message}");
            }

            return topUsers;
        }

        public List<UserStatisticsWithUserInfo> GetTopUsersByLikeRatio(int limit = 5)
        {
            List<UserStatisticsWithUserInfo> topUsers = new List<UserStatisticsWithUserInfo>();

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = @"
                        SELECT us.id_user, us.received_likes, us.received_dislikes, 
                               us.sent_likes, us.sent_dislikes, us.total_matches, 
                               u.first_name, u.last_name, u.birth_date, u.profile_phrase,
                               CASE 
                                   WHEN (us.received_likes + us.received_dislikes) > 0 
                                   THEN us.received_likes::float / (us.received_likes + us.received_dislikes) 
                                   ELSE 0 
                               END AS like_ratio
                        FROM UserStatistics us
                        JOIN Users u ON us.id_user = u.id_user
                        WHERE (us.received_likes + us.received_dislikes) > 5  -- Al menos algunas interacciones
                        ORDER BY like_ratio DESC
                        LIMIT @limit";
                    
                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@limit", limit);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var stat = new UserStatisticsWithUserInfo
                                {
                                    id_user = reader.GetInt32(0),
                                    received_likes = reader.GetInt32(1),
                                    received_dislikes = reader.GetInt32(2),
                                    sent_likes = reader.GetInt32(3),
                                    sent_dislikes = reader.GetInt32(4),
                                    total_matches = reader.GetInt32(5),
                                    first_name = reader.GetString(6),
                                    last_name = reader.GetString(7),
                                    birth_date = reader.GetDateTime(8),
                                    profile_phrase = reader.GetString(9),
                                    like_ratio = reader.GetDouble(10)
                                };
                                topUsers.Add(stat);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener usuarios con mejor ratio: {ex.Message}");
            }

            return topUsers;
        }

        public void EnsureTableExists()
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = @"
                        CREATE TABLE IF NOT EXISTS UserStatistics (
                            id_user INTEGER PRIMARY KEY,
                            received_likes INTEGER NOT NULL DEFAULT 0,
                            received_dislikes INTEGER NOT NULL DEFAULT 0,
                            sent_likes INTEGER NOT NULL DEFAULT 0,
                            sent_dislikes INTEGER NOT NULL DEFAULT 0,
                            total_matches INTEGER NOT NULL DEFAULT 0,
                            last_update TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                            FOREIGN KEY (id_user) REFERENCES Users(id_user)
                        )";
                    
                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al crear tabla de estadísticas: {ex.Message}");
            }
        }
    }

    public class UserStatisticsWithUserInfo : UserStatistics
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime birth_date { get; set; }
        public string profile_phrase { get; set; }
        public double like_ratio { get; set; }

        public int CalculateAge()
        {
            var today = DateTime.Today;
            var age = today.Year - birth_date.Year;
            if (birth_date.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
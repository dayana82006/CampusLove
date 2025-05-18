using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using CampusLove.Domain.Interfaces;
using CampusLove.Domain.Entities;

namespace CampusLove.Infrastructure.Repositories
{
    public class PgsqlMessagesRepository : IMessagesRepository
    {
        private readonly string _connectionString;

        public PgsqlMessagesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Insert(Messages message)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string query = "INSERT INTO messages (sender_id, receiver_id, message_text, message_time) VALUES (@id_user_sender, @id_user_receiver, @message_text, @send_date)";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id_user_sender", message.id_user_sender);
            command.Parameters.AddWithValue("@id_user_receiver", message.id_user_receiver);
            command.Parameters.AddWithValue("@message_text", message.content);
            command.Parameters.AddWithValue("@send_date", message.send_date);

            command.ExecuteNonQuery();
        }

        public void Delete(int messageId)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string query = "DELETE FROM messages WHERE id = @id";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", messageId);

            command.ExecuteNonQuery();
        }

        public IEnumerable<Messages> GetAll()
        {
            var messages = new List<Messages>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string query = "SELECT id, sender_id, receiver_id, message_text, message_time FROM messages ORDER BY message_time ASC";
            using var command = new NpgsqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                messages.Add(new Messages
                {
                    id_message = reader.GetInt32(0),
                    id_user_sender = reader.GetInt32(1),
                    id_user_receiver = reader.GetInt32(2),
                    content = reader.GetString(3),
                    send_date = reader.GetDateTime(4)
                });
            }

            return messages;
        }

        public List<Messages> GetMessagesBetweenUsers(int userId1, int userId2)
        {
            var messages = new List<Messages>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string query = @"
                SELECT id, sender_id, receiver_id, message_text, message_time
                FROM messages
                WHERE (sender_id = @user1 AND receiver_id = @user2)
                   OR (sender_id = @user2 AND receiver_id = @user1)
                ORDER BY message_time ASC";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@user1", userId1);
            command.Parameters.AddWithValue("@user2", userId2);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                messages.Add(new Messages
                {
                    id_message = reader.GetInt32(0),
                    id_user_sender = reader.GetInt32(1),
                    id_user_receiver = reader.GetInt32(2),
                    content = reader.GetString(3),
                    send_date = reader.GetDateTime(4)
                });
            }

            return messages;
        }
    }
}

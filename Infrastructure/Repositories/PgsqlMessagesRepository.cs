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
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void Insert(Messages message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                connection.Open();

                string query = "INSERT INTO messages (sender_id, receiver_id, message_text, message_time) VALUES (@sender_id, @receiver_id, @message_text, @message_time)";
                using var command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@sender_id", message.id_user_sender);
                command.Parameters.AddWithValue("@receiver_id", message.id_user_receiver);
                command.Parameters.AddWithValue("@message_text", message.content);
                command.Parameters.AddWithValue("@message_time", message.send_date);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar mensaje en la base de datos: {ex.Message}", ex);
            }
        }

        public void Delete(int messageId)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                connection.Open();

                string query = "DELETE FROM messages WHERE id = @id";
                using var command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", messageId);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new InvalidOperationException($"No se encontró mensaje con ID {messageId}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar mensaje de la base de datos: {ex.Message}", ex);
            }
        }

        public IEnumerable<Messages> GetAll()
        {
            var messages = new List<Messages>();

            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                connection.Open();

                string query = "SELECT id, sender_id, receiver_id, message_text, message_time FROM messages ORDER BY message_time ASC";
                using var command = new NpgsqlCommand(query, connection);
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    messages.Add(new Messages
                    {
                        id_message = reader.GetInt32("id"),
                        id_user_sender = reader.GetInt32("sender_id"),
                        id_user_receiver = reader.GetInt32("receiver_id"),
                        content = reader.GetString("message_text"),
                        send_date = reader.GetDateTime("message_time")
                    });
                }

                return messages;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener todos los mensajes de la base de datos: {ex.Message}", ex);
            }
        }

        public List<Messages> GetMessagesBetweenUsers(int userId1, int userId2)
        {
            var messages = new List<Messages>();

            try
            {
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
                        id_message = reader.GetInt32("id"),
                        id_user_sender = reader.GetInt32("sender_id"),
                        id_user_receiver = reader.GetInt32("receiver_id"),
                        content = reader.GetString("message_text"),
                        send_date = reader.GetDateTime("message_time")
                    });
                }

                return messages;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener mensajes entre usuarios de la base de datos: {ex.Message}", ex);
            }
        }
    }
}
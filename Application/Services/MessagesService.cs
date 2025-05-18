using System;
using System.Collections.Generic;
using System.Linq;
using CampusLove.Domain.Entities;         // Asegúrate de que exista Messages.cs aquí
using CampusLove.Domain.Interfaces;       // Asegúrate de que exista IMessagesRepository.cs aquí

namespace CampusLove.Application.Services
{
    public class MessagesService
    {
        private readonly IMessagesRepository _messagesRepository;
        private readonly MatchesService _matchesService;

        public MessagesService(IMessagesRepository messagesRepository)
        {
            _messagesRepository = messagesRepository;
        }

        public bool SendMessage(int senderId, int receiverId, string message)
        {
            if (!_matchesService.IsMatch(senderId, receiverId))
            {
                Console.WriteLine("❌ No se puede enviar el mensaje porque no hay match.");
                return false;
            }

            var msg = new Messages
            {
                id_user_sender = senderId,
                id_user_receiver = receiverId,
                content = message,
                send_date = DateTime.Now
            };

            _messagesRepository.Insert(msg);
            Console.WriteLine("✅ Mensaje enviado.");
            return true;
        }

        public List<Messages> GetConversation(int userId1, int userId2)
        {
            return _messagesRepository.GetAll()
                .Where(m =>
                    (m.id_user_sender == userId1 && m.id_user_receiver == userId2) ||
                    (m.id_user_sender == userId2 && m.id_user_receiver == userId1))
                .OrderBy(m => m.send_date)
                .ToList();
        }

        public List<Messages> GetReceivedMessages(int userId)
        {
            return _messagesRepository.GetAll()
                .Where(m => m.id_user_receiver == userId)
                .OrderByDescending(m => m.send_date)
                .ToList();
        }

        public List<Messages> GetSentMessages(int userId)
        {
            return _messagesRepository.GetAll()
                .Where(m => m.id_user_sender == userId)
                .OrderByDescending(m => m.send_date)
                .ToList();
        }

        public void DeleteMessage(int messageId)
        {
            _messagesRepository.Delete(messageId);
            Console.WriteLine($"🗑️ Mensaje {messageId} eliminado.");
        }

        public List<Messages> GetUserMessageHistory(int userId)
        {
            return _messagesRepository.GetAll()
                .Where(m => m.id_user_sender == userId || m.id_user_receiver == userId)
                .OrderByDescending(m => m.send_date)
                .ToList();
        }
        public List<Messages> GetMessagesBetweenUsers(int userId1, int userId2)
        {
            return _messagesRepository.GetMessagesBetweenUsers(userId1, userId2);
        }
    public void VisualizarMensajes(int currentUserId, int matchedUserId)
{
    while (true)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\t ♥ ¸.•*¨*•♫♪♥ MIS MENSAJES ♥ ♪♫•*¨*•.¸ ♥");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("╔═══════════════════════════════════╗");
        Console.WriteLine("║          📬 MENSAJES 📬           ║");
        Console.WriteLine("╚═══════════════════════════════════╝");
        Console.ResetColor();

        var messages = GetMessagesBetweenUsers(currentUserId, matchedUserId)
                        .OrderBy(m => m.send_date)
                        .ToList();

        if (!messages.Any())
        {
            Console.WriteLine("\nNo hay mensajes aún. ¡Sé el primero en escribir!");
        }
        else
        {
            foreach (var msg in messages)
            {
                var sender = msg.id_user_sender == currentUserId ? "Tú" : "Match";
                Console.WriteLine($"\n[{msg.send_date:dd/MM/yyyy HH:mm}] {sender}: {msg.content}");
            }
        }

        Console.WriteLine(@"
    ¿Qué deseas hacer?
        [E] Escribir mensaje
        [R] Regresar
    ");
        var option = Console.ReadLine()?.Trim().ToUpper();

        if (option == "E")
        {
            Console.Write("\nEscribe tu mensaje: ");
            var content = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(content))
            {
                SendMessage(currentUserId, matchedUserId, content);
                Console.WriteLine("✅ Mensaje enviado.");
                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine("⚠️ El mensaje no puede estar vacío.");
                Thread.Sleep(1000);
            }
        }
        else if (option == "R")
        {
            break;
        }
        else
        {
            Console.WriteLine("⚠️ Opción no válida.");
            Thread.Sleep(1000);
        }
    }
}

    }
}

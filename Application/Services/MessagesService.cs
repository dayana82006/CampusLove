using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Application.Services
{
    public class MessagesService
{
    private IMessagesRepository _messagesRepository;
    private MatchesService _matchesService;

    public MessagesService(IMessagesRepository messagesRepository)
    {
        _messagesRepository = messagesRepository ?? throw new ArgumentNullException(nameof(messagesRepository));
    }

    public void SetMatchesService(MatchesService matchesService)
    {
        _matchesService = matchesService ?? throw new ArgumentNullException(nameof(matchesService));
    }
    
        public bool SendMessage(int senderId, int receiverId, string message)
        {
            try
            {
                // Validar que no sea el mismo usuario
                if (senderId == receiverId)
                {
                    Console.WriteLine("❌ No puedes enviarte mensajes a ti mismo.");
                    return false;
                }

                // Validar que hay match
                if (!_matchesService.IsMatch(senderId, receiverId))
                {
                    Console.WriteLine("❌ No se puede enviar el mensaje porque no hay match.");
                    return false;
                }

                // Validar que el mensaje no esté vacío
                if (string.IsNullOrWhiteSpace(message))
                {
                    Console.WriteLine("❌ El mensaje no puede estar vacío.");
                    return false;
                }

                // Validar longitud del mensaje
                if (message.Length > 250)
                {
                    Console.WriteLine("❌ El mensaje no puede tener más de 250 caracteres.");
                    return false;
                }

                var msg = new Messages
                {
                    id_user_sender = senderId,
                    id_user_receiver = receiverId,
                    content = message.Trim(),
                    send_date = DateTime.Now
                };

                _messagesRepository.Insert(msg);
                Console.WriteLine("✅ Mensaje enviado correctamente.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al enviar mensaje: {ex.Message}");
                return false;
            }
        }

        public List<Messages> GetConversation(int userId1, int userId2)
        {
            try
            {
                return _messagesRepository.GetAll()
                    .Where(m =>
                        (m.id_user_sender == userId1 && m.id_user_receiver == userId2) ||
                        (m.id_user_sender == userId2 && m.id_user_receiver == userId1))
                    .OrderBy(m => m.send_date)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener conversación: {ex.Message}");
                return new List<Messages>();
            }
        }

        public List<Messages> GetReceivedMessages(int userId)
        {
            try
            {
                return _messagesRepository.GetAll()
                    .Where(m => m.id_user_receiver == userId)
                    .OrderByDescending(m => m.send_date)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener mensajes recibidos: {ex.Message}");
                return new List<Messages>();
            }
        }

        public List<Messages> GetSentMessages(int userId)
        {
            try
            {
                return _messagesRepository.GetAll()
                    .Where(m => m.id_user_sender == userId)
                    .OrderByDescending(m => m.send_date)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener mensajes enviados: {ex.Message}");
                return new List<Messages>();
            }
        }

        public void DeleteMessage(int messageId)
        {
            try
            {
                _messagesRepository.Delete(messageId);
                Console.WriteLine($"🗑️ Mensaje {messageId} eliminado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al eliminar mensaje: {ex.Message}");
            }
        }

        public List<Messages> GetUserMessageHistory(int userId)
        {
            try
            {
                return _messagesRepository.GetAll()
                    .Where(m => m.id_user_sender == userId || m.id_user_receiver == userId)
                    .OrderByDescending(m => m.send_date)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener historial de mensajes: {ex.Message}");
                return new List<Messages>();
            }
        }

        public List<Messages> GetMessagesBetweenUsers(int userId1, int userId2)
        {
            try
            {
                return _messagesRepository.GetMessagesBetweenUsers(userId1, userId2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener mensajes entre usuarios: {ex.Message}");
                return new List<Messages>();
            }
        }

        public void VisualizarMensajes(int currentUserId, int matchedUserId)
        {
            while (true)
            {
                try
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
                            var color = msg.id_user_sender == currentUserId 
                                ? ConsoleColor.Cyan 
                                : ConsoleColor.Yellow;
                            
                            Console.ForegroundColor = color;
                            Console.WriteLine($"\n[{msg.send_date:dd/MM/yyyy HH:mm}] {sender}: {msg.content}");
                            Console.ResetColor();
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
                            if (SendMessage(currentUserId, matchedUserId, content))
                            {
                                Thread.Sleep(1500); // Pausa para mostrar el mensaje de éxito
                            }
                            else
                            {
                                Thread.Sleep(2000); // Pausa más larga para errores
                            }
                        }
                        else
                        {
                            Console.WriteLine("⚠️ El mensaje no puede estar vacío.");
                            Thread.Sleep(1500);
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
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error inesperado: {ex.Message}");
                    Thread.Sleep(2000);
                }
            }
        }
    }
}
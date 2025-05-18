using System;
using CampusLove.Application.Services;
using CampusLove.Domain.Entities;

namespace CampusLove.Application.UI.User
{
    public class StatisticsViewer
    {
        
        private readonly UserStatisticsService _userStatisticsService;
        private readonly dynamic _currentUser;

        public StatisticsViewer(UserStatisticsService userStatisticsService, dynamic currentUser)
        {
            _userStatisticsService = userStatisticsService;
            _currentUser = currentUser;
        }

        public void DisplayStatistics()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\t 🌟 ESTADÍSTICAS DEL AMOR 🌟");
            Console.ResetColor();

            var statistics = _userStatisticsService.GetUserStatistics((int)_currentUser.id_user);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($@"
♥ ¸.•*¨*•♫♪♥ MIS ESTADISTICAS ♥ ♪♫•*¨*•.¸ ♥

👤 Usuario:        {_currentUser.first_name} {_currentUser.last_name}
📅 Última actualización: {statistics.last_update:dd/MM/yyyy HH:mm}
");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($@"
💘 Likes enviados:         {statistics.sent_likes}
💔 Dislikes enviados:      {statistics.sent_dislikes}
🎯 Likes recibidos:        {statistics.received_likes}
💢 Dislikes recibidos:     {statistics.received_dislikes}
❤️ Total de matches:       {statistics.total_matches}
");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("✨ Sigue interactuando, ¡el amor está a un like de distancia!");
            Console.ResetColor();

            Console.WriteLine("\nPresiona cualquier tecla para volver al menú principal...");
            Console.ReadKey();
        }
    }
}

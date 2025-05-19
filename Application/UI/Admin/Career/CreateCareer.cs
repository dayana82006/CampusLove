using System;
using CampusLove.Domain.Entities;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.Admin.Career
{
    public class CreateCareer
    {
        private readonly CareersService _servicio;

        public CreateCareer(CareersService servicio)
        {
            _servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));
        }

        public void Ejecutar()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║        🎓 CREAR NUEVA CARRERA       ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.ResetColor();

            var career = new Careers();

            Console.Write("\n📌 Nombre de la carrera: ");
            career.career_name = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(career.career_name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ El nombre no puede estar vacío. Intenta nuevamente.");
                Console.ResetColor();
                return;
            }

            _servicio.CreateCareer(career);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Carrera creada con éxito. ¡Buena suerte a los futuros estudiantes!");
            Console.ResetColor();
        }
    }
}

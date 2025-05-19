using System;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.Admin.City
{
    public class DeleteCity
    {
        private readonly CityService _servicio;

        public DeleteCity(CityService servicio)
        {
            _servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));
        }

        public void Ejecutar()
        {
            Console.Write("Ingrese el ID de la ciudad a eliminar: ");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;

            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("❌ ID inválido. Debe ser un número entero.");
                return;
            }

            var ciudadExistente = _servicio.GetById(id);
            if (ciudadExistente == null)
            {
                Console.WriteLine("❌ No se encontró ninguna ciudad con ese ID.");
                return;
            }

            Console.Write($"¿Estás seguro de que deseas eliminar la ciudad '{ciudadExistente.city_name}'? (s/n): ");
            var confirmacion = Console.ReadLine()?.Trim().ToLower();

            if (confirmacion == "s")
            {
                bool resultado = _servicio.DeleteCity(id);
                if (resultado)
                    Console.WriteLine("✅ Ciudad eliminada exitosamente.");
                else
                    Console.WriteLine("❌ No se pudo eliminar la ciudad.");
            }
            else
            {
                Console.WriteLine("❌ Operación cancelada.");
            }
        }
    }
}

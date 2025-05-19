using System;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.Admin.Countries
{
    public class UpdateCountry
    {
        private readonly CountryService _servicio;

        public UpdateCountry(CountryService servicio)
        {
            _servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));
        }

        public void Ejecutar()
        {
            Console.Write("ID del país a actualizar: ");
            
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("❌ ID no válido. Debe ser un número entero.");
                return;
            }

            Console.Write("Nuevo nombre: ");
            string nuevoNombre = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(nuevoNombre))
            {
                Console.WriteLine("❌ El nombre no puede estar vacío.");
                return;
            }

            bool actualizado = _servicio.UpdateCountry(id, nuevoNombre);

            if (actualizado)
            {
                Console.WriteLine("✅ País actualizado con éxito.");
            }
            else
            {
                Console.WriteLine("❌ No se encontró un país con ese ID.");
            }
        }
    }
}

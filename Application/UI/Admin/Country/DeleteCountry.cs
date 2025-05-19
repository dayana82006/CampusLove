using System;
using CampusLove.Domain.Entities;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.Admin.Countries
{
    public class DeleteCountry
    {
        private readonly CountryService _servicio;

        public DeleteCountry(CountryService servicio)
        {
            _servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));
        }

        public void Ejecutar()
        {
            Console.Write("Ingrese el ID del país a eliminar: ");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;

            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("❌ ID inválido. Debe ser un número entero.");
                return;
            }

            
        }
    }
}

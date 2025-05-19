using System;
using CampusLove.Domain.Entities;
using CampusLove.Application.Services;
using CampusLove.Domain.Interfaces;
namespace CampusLove.Application.UI.Admin.Countries
{
    public class CreateCountry
    {
        private readonly CountryService _servicio;

        public CreateCountry(CountryService servicio)
        {
            _servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));
        }
 public void Ejecutar()
        {
            var country = new Country();

            Console.Write("Nombre del país: ");
            country.country_name = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(country.country_name))
            {
                Console.WriteLine("❌ El nombre no puede estar vacío.");
                return;
            }

            _servicio.CreateCountry(country); // Usa await si puedes
            Console.WriteLine("✅ País creado con éxito.");
        }
    }
}

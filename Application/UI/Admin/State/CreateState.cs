using System;
using CampusLove.Domain.Entities;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.Admin.Countries
{
    public class CreateState
    {
        private readonly StateService _servicio;
        private readonly CountryService _countryService;

        public CreateState(StateService servicio, CountryService countryService)
        {
            _servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));
            _countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
        }

        public void Ejecutar()
        {
            var state = new States();

            Console.Write("Nombre del estado: ");
            state.state_name = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(state.state_name))
            {
                Console.WriteLine("❌ El nombre no puede estar vacío.");
                return;
            }

            // Mostrar lista de países
            Console.WriteLine("\n📋 Lista de países disponibles:");
            _countryService.ShowAll();

            Console.Write("ID del país al que pertenece este estado: ");
            if (!int.TryParse(Console.ReadLine(), out int countryId))
            {
                Console.WriteLine("❌ ID inválido. Debe ser un número entero.");
                return;
            }

            var country = _countryService.GetById(countryId);
            if (country == null)
            {
                Console.WriteLine("❌ País no encontrado. Asegúrate de que el ID existe.");
                return;
            }

            state.id_country = countryId;

            _servicio.CreateState(state);
            Console.WriteLine("✅ Estado creado con éxito.");
        }
    }
}

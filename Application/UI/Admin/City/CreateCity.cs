using System;
using CampusLove.Domain.Entities;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.Admin.City
{
    public class CreateCity
    {
        private readonly CityService _servicio;
        private readonly StateService _stateService;

        public CreateCity(CityService servicio, StateService stateService)
        {
            _servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));
            _stateService = stateService ?? throw new ArgumentNullException(nameof(stateService));
        }
        public void Ejecutar()
        {
            var city = new Cities();

            Console.Write("Nombre de la ciudad: ");
            city.city_name = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(city.city_name))
            {
                Console.WriteLine("❌ El nombre no puede estar vacío.");
                return;
            }

            Console.Write("ID del estado al que pertenece: ");
            if (!int.TryParse(Console.ReadLine(), out int stateId))
            {
                Console.WriteLine("❌ ID inválido. Debe ser un número entero.");
                return;
            }



            city.id_state = stateId;

            if (_servicio.CreateCity(city))
            {
                Console.WriteLine($"✅ Ciudad '{city.city_name}' creada con éxito.");
            }
            else
            {
                Console.WriteLine("❌ No se pudo crear la ciudad.");
            }
        }
    }

}

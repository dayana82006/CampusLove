using System;
using CampusLove.Application.Services;
using CampusLove.Domain.Entities;

namespace CampusLove.Application.UI.Admin.City
{
    public class UpdateCity
    {
        private readonly CityService _cityService;
        private readonly StateService _stateService;

        public UpdateCity(CityService cityService, StateService stateService)
        {
            _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
            _stateService = stateService ?? throw new ArgumentNullException(nameof(stateService));
        }

        public void Ejecutar()
        {
            Console.Write("Ingrese el ID de la ciudad a actualizar: ");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;

            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("❌ ID inválido. Debe ser un número entero.");
                return;
            }

            var ciudad = _cityService.GetById(id);
            if (ciudad == null)
            {
                Console.WriteLine("❌ Ciudad no encontrada.");
                return;
            }

            Console.WriteLine($"✏️ Nombre actual: {ciudad.city_name}");
            Console.Write("Ingrese el nuevo nombre de la ciudad (deje vacío para no cambiar): ");
            var nuevoNombre = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(nuevoNombre))
            {
                ciudad.city_name = nuevoNombre;
            }

            // Mostrar lista de estados
            var estados = _stateService.GetAll();
            if (estados.Count == 0)
            {
                Console.WriteLine("❌ No hay estados disponibles para asignar.");
                return;
            }

            Console.WriteLine("Estados disponibles:");
            foreach (var estado in estados)
            {
                Console.WriteLine($"ID: {estado.id_state}, Nombre: {estado.state_name}");
            }

            Console.Write($"Ingrese el ID del nuevo estado para la ciudad (actual: {ciudad.id_state}, deje vacío para no cambiar): ");
            var estadoInput = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(estadoInput))
            {
                if (!int.TryParse(estadoInput, out int nuevoEstadoId))
                {
                    Console.WriteLine("❌ ID de estado inválido.");
                    return;
                }

                var estadoSeleccionado = _stateService.GetById(nuevoEstadoId);
                if (estadoSeleccionado == null)
                {
                    Console.WriteLine("❌ Estado no encontrado.");
                    return;
                }

                ciudad.id_state = nuevoEstadoId;
            }

            bool resultado = _cityService.UpdateCity(ciudad.id_city, ciudad.city_name);

            if (resultado)
            {
                Console.WriteLine("✅ Ciudad actualizada correctamente.");
            }
            else
            {
                Console.WriteLine("❌ No se pudo actualizar la ciudad.");
            }
        }
    }
}

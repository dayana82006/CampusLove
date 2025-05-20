using System;
using CampusLove.Application.Services;
using CampusLove.Domain.Entities;

namespace CampusLove.UI.Admin.Adress
{
    public class UpdateAdress
    {
        private readonly AddressesService _service;

        public UpdateAdress(AddressesService service)
        {
            _service = service;
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("=== ACTUALIZAR DIRECCIÓN ===");

            Console.Write("Ingrese ID de la dirección a actualizar: ");
            int id = int.Parse(Console.ReadLine());

            var existing = _service.GetById(id);
            if (existing == null)
            {
                Console.WriteLine("Dirección no encontrada.");
                return;
            }

            Console.WriteLine($"Dirección actual: {existing.street_number} {existing.street_name}");

            Console.Write("Nuevo número de calle (enter para mantener): ");
            var newNumber = Console.ReadLine();

            Console.Write("Nuevo nombre de calle (enter para mantener): ");
            var newName = Console.ReadLine();

            existing.street_number = string.IsNullOrWhiteSpace(newNumber) ? existing.street_number : newNumber;
            existing.street_name = string.IsNullOrWhiteSpace(newName) ? existing.street_name : newName;

            _service.ObtenerOCrearDireccion(existing); // puedes reemplazar esto por Update si implementas un método específico

            Console.WriteLine("Dirección actualizada con éxito.");
        }
    }
}

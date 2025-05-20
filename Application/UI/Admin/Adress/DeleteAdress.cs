using System;
using CampusLove.Application.Services;

namespace CampusLove.UI.Admin.Adress
{
    public class DeleteAdress
    {
        private readonly AddressesService _service;

        public DeleteAdress(AddressesService service)
        {
            _service = service;
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("=== ELIMINAR DIRECCIÓN ===");

            Console.Write("Ingrese ID de la dirección a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            var existing = _service.GetById(id);
            if (existing == null)
            {
                Console.WriteLine("Dirección no encontrada.");
                return;
            }
            Console.Write("¿Está seguro de eliminar la dirección? (s/n): ");
            if (Console.ReadLine()?.ToLower() == "s")
            {
                _service.Delete(id); 
                Console.WriteLine("Dirección eliminada.");
            }
            else
            {
                Console.WriteLine("Operación cancelada.");
            }
        }
    }
}

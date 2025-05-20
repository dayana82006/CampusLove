using System;
using CampusLove.Application.Services;
using CampusLove.Domain.Entities;

namespace CampusLove.UI.Admin.Adress
{
    public class CreateAdress
    {
        private readonly AddressesService _service;

        public CreateAdress(AddressesService service)
        {
            _service = service;
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("=== CREAR DIRECCIÓN ===");

            Console.Write("Ingrese ID de la ciudad: ");
            int id_city = int.Parse(Console.ReadLine());

            Console.Write("Ingrese número de calle: ");
            string streetNumber = Console.ReadLine();

            Console.Write("Ingrese nombre de la calle: ");
            string streetName = Console.ReadLine();

            var address = new Addresses
            {
                id_city = id_city,
                street_number = streetNumber,
                street_name = streetName
            };

            _service.CrearAddress(address);
            Console.WriteLine("Dirección creada con éxito.");
        }
    }
}

using System;
using CampusLove.Application.Services;
using CampusLove.UI.Admin.Adress;

namespace CampusLove.Application.UI.Admin.Adress
{
    public class UIAdress
    {
        private readonly AddressesService _service;

        public UIAdress(AddressesService service)
        {
            _service = service;
        }

        public void ShowMenu()
        {
            int option;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("✧˖°. 💌 G E S T I Ó N   D E   D I R E C C I O N E S 💌 .°˖✧\n");
                Console.ResetColor();
                Console.WriteLine("1. 🏡 Crear dirección");
                Console.WriteLine("2. 🛠️ Actualizar dirección");
                Console.WriteLine("3. ❌ Eliminar dirección");
                Console.WriteLine("4. 📜 Ver todas las direcciones");
                Console.WriteLine("0. 🚪 Volver");
                Console.Write("\n💗 Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out option)) option = -1;

                switch (option)
                {
                    case 1:
                        new CreateAdress(_service).Run();
                        break;
                    case 2:
                        new UpdateAdress(_service).Run();
                        break;
                    case 3:
                        new DeleteAdress(_service).Run();
                        break;
                    case 4:
                        ShowAll();
                        break;
                    case 0:
                        Console.WriteLine("\n👋 ¡Hasta pronto, administrador brillante!");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n⚠️ Opción no válida, intenta nuevamente.");
                        Console.ResetColor();
                        break;
                }

                if (option != 0)
                {
                    Console.WriteLine("\n✨ Presiona cualquier tecla para continuar...");
                    Console.ReadKey();
                }

            } while (option != 0);
        }

        private void ShowAll()
        {
            Console.Clear();
            var addresses = _service.GetAll();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("🌍 Lista encantadora de direcciones registradas:\n");
            Console.ResetColor();

            foreach (var address in addresses)
            {
                var full = _service.GetFullAddress(address.id_address);
                Console.WriteLine($"📍 ID: {address.id_address} | Dirección: {address.street_number} {address.street_name} | {full}");
            }

            Console.WriteLine("\n💫 Fin de la lista 💫");
        }
    }
}

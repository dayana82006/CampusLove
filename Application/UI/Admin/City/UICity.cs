using System;
using CampusLove.Application.Services;
using CampusLove.Domain.Ports;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Application.UI.Admin.City
{
    public class UICity
    {
        private readonly CityService _servicio;
        private readonly StateService _stateService;

        public UICity(IDbFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            var cityRepo = factory.CreateCitiesRepository();
            var stateRepo = factory.CreateStatesRepository();

            _servicio = new CityService(cityRepo, stateRepo); 
            _stateService = new StateService(stateRepo);      
        }


        public void GestionarCities()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("             ⋆｡‧˚ʚ🌆ɞ˚‧｡⋆");
                Console.WriteLine("   ⭒❃.✮:▹ Menú de Ciudades ◃:✮.❃⭒");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(" ⤜♡→ 1. Ver todas las ciudades 🏙️");
                Console.WriteLine(" ⤜♡→ 2. Agregar una ciudad nueva 🏗️");
                Console.WriteLine(" ⤜♡→ 3. Actualizar una ciudad ✏️");
                Console.WriteLine(" ⤜♡→ 4. Eliminar una ciudad ❌");
                Console.WriteLine(" ⤜♡→ 0. Volver al menú principal ↩️");
                Console.ResetColor();

                var opcion = Console.ReadLine();

                if (string.IsNullOrEmpty(opcion))
                {
                    Console.WriteLine("❌ Debes ingresar una opción.");
                    Console.ReadKey();
                    continue;
                }

                try
                {
                    switch (opcion)
                    {
                        case "1":
                            _servicio.ShowAll();
                            break;
                        case "2":
                            var crear = new CreateCity(_servicio, _stateService);
                            crear.Ejecutar();
                            break;
                        case "3":
                            var actualizar = new UpdateCity(_servicio, _stateService);
                            actualizar.Ejecutar();
                            break;
                        case "4":
                            var eliminar = new DeleteCity(_servicio);
                            eliminar.Ejecutar();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("❌ Opción no válida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ Ocurrió un error: {ex.Message}");
                }

                Console.WriteLine("\nPresiona una tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}

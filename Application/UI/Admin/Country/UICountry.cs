using System;
using CampusLove.Application.Services;
using CampusLove.Domain.Ports;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;
using Mysqlx.Crud;
using CampusLove.Application.UI.Admin.Countries;

namespace CampusLove.Application.UI.Admin.Countries
{
    public class UICountry
    {
        private readonly CountryService _servicio;

        public UICountry(IDbFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _servicio = new CountryService(factory.CreateCountryRepository());
        } 

        public void GestionPaises()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("             ⋆｡‧˚ʚ🍒ɞ˚‧｡⋆");
                Console.WriteLine("   ⭒❃.✮:▹ Menú de opciones ◃:✮.❃⭒");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" ⤜♡→ 1. Ver todos los países 🌍");
                Console.WriteLine(" ⤜♡→ 2. Agregar un nuevo país ✨");
                Console.WriteLine(" ⤜♡→ 3. Actualizar información 📋");
                Console.WriteLine(" ⤜♡→ 4. Eliminar un país 💔");
                Console.WriteLine(" ⤜♡→ 0. Volver al menú principal ↩️");
                Console.ResetColor();

                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        _servicio.ShowAll(); // Si no puedes usar await
                        break;
                    case "2":
                        var crear = new CreateCountry(_servicio);
                        crear.Ejecutar();
                        break;
                    case "3":
                        var actualizar = new UpdateCountry(_servicio);
                        actualizar.Ejecutar();
                        break;
                    case "4":
                        var eliminar = new DeleteCountry(_servicio);
                        eliminar.Ejecutar();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("❌ Opción no válida.");
                        break;
                }

                Console.WriteLine("\nPresiona una tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}

using CampusLove.Application.Services;
using CampusLove.Application.UI.Admin.Countries;
using CampusLove.Application.UI.Admin.State;
using CampusLove.Domain.Interfaces;

public class UIState
{
    private readonly StateService _servicio;
    private readonly CountryService _countryService;

    public UIState(IDbFactory factory)
    {
        if (factory == null)
            throw new ArgumentNullException(nameof(factory));

        _servicio = new StateService(factory.CreateStatesRepository());
        _countryService = new CountryService(factory.CreateCountryRepository());
    }

    public void GestionarStates()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("             ⋆｡‧˚ʚ🍒ɞ˚‧｡⋆");
            Console.WriteLine("   ⭒❃.✮:▹ Menú de opciones ◃:✮.❃⭒");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" ⤜♡→ 1. Ver todos los Estados 🌍");
            Console.WriteLine(" ⤜♡→ 2. Agregar un nuevo Estado ✨");
            Console.WriteLine(" ⤜♡→ 3. Actualizar información 📋");
            Console.WriteLine(" ⤜♡→ 4. Eliminar un Estado 💔");
            Console.WriteLine(" ⤜♡→ 0. Volver al menú principal ↩️");
            Console.ResetColor();

            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    _servicio.ShowAll(); 
                    break;
                case "2":
                    var crear = new CreateState(_servicio, _countryService); // 👈 aquí está el cambio
                    crear.Ejecutar();
                    break;
                case "3":
                    var actualizar = new UpdateState(_servicio);
                    actualizar.Ejecutar();
                    break;
                case "4":
                    var eliminar = new DeleteState(_servicio);
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

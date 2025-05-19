using CampusLove.Application.Services;
using CampusLove.Application.UI.Admin.City;
using CampusLove.Application.UI.Admin.Countries;
using CampusLove.Application.UI.Admin.Career;
using CampusLove.Application.UI.Admin.Users;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Application.UI
{
    public class AdminUI
    {
        private readonly IDbFactory _factory;

        public AdminUI(IDbFactory factory)
        {
            _factory = factory;
        }

        public static string Menu()
        {
            return "1. 🌟 Manejo Usuarios\n" +
                   "2. 🎓 Manejo Carreras\n" +
                   "3. 💖 Manejo Interés\n" +
                   "4. 🏠 Manejo Dirección\n" +
                   "5. 🌆 Manejo Ciudad\n" +
                   "6. 🌍 Manejo País\n" +
                   "7. 🗺️ Manejo Estado\n" +
                   "8. 👥 Manejo Género\n" +
                   "0. 🚪 Salir\n";
        }

        public void MenuAdmin()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("⋆｡ﾟ☁︎｡⋆｡ ﾟ☾ ﾟ｡⋆｡ﾟ☁︎｡⋆｡  ﾟ｡⋆｡⋆｡ ﾟ☾｡☁︎｡⋆｡ ﾟ☾☾｡⋆｡ ");
            Console.WriteLine("     💌 C A M P U S   L O V E 💌");
            Console.WriteLine("     ❝ BIENVENIDO ADMINISTRADOR ❞");
            Console.WriteLine("⋆｡ﾟ☁︎｡⋆｡ ﾟ☾ ﾟ｡⋆☁︎｡⋆｡ ﾟ☾｡ ﾟ☁︎｡⋆｡ ﾟ☾ ﾟ｡⋆｡ ﾟ｡ﾟ☁︎｡⋆\n");
            Console.ResetColor();

            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine(Menu());
                Console.Write("💗 Seleccione una opción 💗 : ");
                int opcion = Utilidades.LeerOpcionMenuKey(Menu());

                switch (opcion)
                {
                    case 1:
                        EjecutarManejoUsuarios();
                        break;
                    case 2:
                        var uiCareer = new UICareer(_factory);
                        uiCareer.GestionarCareers();
                        
                        break;
                    case 3:
                        Console.WriteLine("🚧 Opción en desarrollo. Pronto disponible.");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.WriteLine("🚧 Opción en desarrollo. Pronto disponible.");
                        Console.ReadKey();
                        break;
                    case 5:
                        var uiCity = new UICity(_factory);
                        uiCity.GestionarCities();
                        break;
                    case 6:
                        var uiCountry = new UICountry(_factory);
                        uiCountry.GestionPaises();
                        break;
                    case 7:
                        var uiState = new UIState(_factory);
                        uiState.GestionarStates();
                        break;
                    case 8:
                        Console.WriteLine("🚧 Opción en desarrollo. Pronto disponible.");
                        Console.ReadKey();
                        break;
                    case 0:
                        Console.Write("\n¿Está seguro que desea salir? 🥺 (S/N): ");
                        salir = Utilidades.LeerTecla(); // Debe devolver true si confirma salir
                        if (salir)
                        {
                            Console.Clear();
                            Console.WriteLine("\n👋 ¡Vuelve Pronto! 👋");
                        }
                        break;
                    default:
                        Console.WriteLine("⚠️ Opción no válida. ⚠️");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void EjecutarManejoUsuarios()
        {
            var userRepo = _factory.CreateUsersRepository();
            var creditsRepo = _factory.CreateInteractionCreditsRepository();
            var interactionsRepo = _factory.CreateInteractionsRepository();
            var matchesRepo = _factory.CreateMatchesRepository();

            var userService = new UserService(userRepo, creditsRepo, interactionsRepo, matchesRepo);
            var genderService = new GendersService(_factory.CreateGendersRepository());
            var careerService = new CareersService(_factory.CreateCareersRepository());
            var connStr = "Host=localhost;Username=postgres;Password=1234;Database=CampusLove"; // Ejemplo

            var addressService = new AddressesService(
                _factory.CreateAddressesRepository(),
                connStr
            );

            var uiUser = new UIManageusers(_factory, userService, genderService, careerService, addressService);
            uiUser.GestionUsers();
        }
    }
}

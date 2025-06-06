using System;
using CampusLove.Application.Services;

using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;
using CampusLove.Infrastructure.Factories;
using CampusLove.Domain.Ports;
using CampusLove.Application.UI.User;
using CampusLove.Application.UI.Admin;

namespace CampusLove.Application.UI
{
    /* INFO: Login inicial donde se inyectan los servicios y repositorios
    con su interfaz agradable al publico y ambientada en el amor de campus love 
    psdt: Es el  mismo amor que le metimos a etse proyecto
    */
    public class LoginUI
    {
        private readonly AuthService _repo;
        private readonly UserService _userService;
        private readonly GendersService _genderService;
        private readonly CareersService _careerService;
        private readonly AddressesService _addressService;
        private readonly InterestsService _interestService;
        private readonly UsersInterestsService _userInterestService;
        private readonly InteractionsService _interactionsService;
        private readonly InteractionCreditsService _creditsService;
        private readonly MatchesService _matchesService;
        private readonly UserStatisticsService _userStatisticsService;
        private readonly MessagesService _messagesService;

        public LoginUI(
            AuthService repo,
            UserService userService,
            InterestsService interestService,
            UsersInterestsService userInterestService,
            GendersService genderService,
            CareersService careerService,
            AddressesService addressService,
            InteractionsService interactionsService,
            InteractionCreditsService creditsService,
            MatchesService matchesService,
            MessagesService messagesService,
            UserStatisticsService userStatisticsService)
        {
            _repo = repo;
            _userService = userService;
            _interestService = interestService;
            _userInterestService = userInterestService;
            _genderService = genderService;
            _careerService = careerService;
            _addressService = addressService;
            _interactionsService = interactionsService;
            _creditsService = creditsService;
            _matchesService = matchesService;
            _messagesService = messagesService;
            _userStatisticsService = userStatisticsService;
        }



        private static void MostrarTitulo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine("⋆｡ﾟ☁︎｡⋆｡ ﾟ☾ ﾟ｡⋆｡ﾟ☁︎｡⋆｡  ﾟ｡⋆｡⋆｡ ﾟ☾｡☁︎｡⋆｡ ﾟ☾☾｡⋆｡ ");
            Console.WriteLine("      💌 C A M P U S   L O V E 💌");
            Console.WriteLine("          ❝ where hearts meet ❞");
            Console.WriteLine("⋆｡ﾟ☁︎｡⋆｡ ﾟ☾ ﾟ｡⋆☁︎｡⋆｡ ﾟ☾｡ ﾟ☁︎｡⋆｡ ﾟ☾ ﾟ｡⋆｡ ﾟ｡ﾟ☁︎｡⋆\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("   𖦹 ୨୧ 💗 𝒃𝒆 𝒃𝒓𝒂𝒗𝒆, 𝒃𝒆 𝒍𝒐𝒗𝒆𝒅 💗 ୨୧ 𖦹\n");

            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine("♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥");
            Console.WriteLine("♥                                       ♥");
            Console.WriteLine("♥           W H E R E   I S             ♥");
            Console.WriteLine("♥              L O V E ?                ♥");
            Console.WriteLine("♥                                       ♥");
            Console.WriteLine("♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥♥");
            Console.WriteLine();

            Console.ResetColor();
        }

        public static string LeerContraseniaOculta()
        {
            string contrasenia = "";
            ConsoleKeyInfo tecla;

            do
            {
                tecla = Console.ReadKey(true);

                if (tecla.Key != ConsoleKey.Backspace && tecla.Key != ConsoleKey.Enter)
                {
                    contrasenia += tecla.KeyChar;
                    Console.Write("♥ ");
                }
                else if (tecla.Key == ConsoleKey.Backspace && contrasenia.Length > 0)
                {
                    contrasenia = contrasenia[..^1];
                    Console.Write("\b \b");
                }
            } while (tecla.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return contrasenia;
        }

        private static string MostrarOpciones()
        {
            return
                "1. Iniciar Sesión\n" +
                "2. Registrarse\n" +
                "0. Salir\n";
        }

        public void MostrarMenu()
        {
            bool salir = false;
            while (!salir)
            {
                MostrarTitulo();
                Console.WriteLine(MostrarOpciones());
                Console.WriteLine("💗 Seleccione una opción 💗 : ");
                int opcion = Utilidades.LeerOpcionMenuKey(MostrarOpciones());

                switch (opcion)
                {
                    case 1:
                        IniciarSesion();
                        break;
                    case 2:
                        Console.Clear();
                        var creadorUsuario = new CreateUser(
                            _userService,
                            _genderService,
                            _careerService,
                            _addressService,
                            _interestService,
                            _userInterestService
                        );
                        creadorUsuario.Ejecutar();
                        break;

                    case 0:
                        Console.WriteLine("\n¿Está seguro que desea salir? 🥺 (S/N): ");
                        salir = Utilidades.LeerTecla();
                        Console.Clear();
                        Console.WriteLine("\n👋 ¡Vuelve pronto! 👋");
                        break;
                    default:
                        Console.WriteLine("⚠️ Opción no válida. ⚠️");
                        break;
                }
            }
        }

        // Lógica para iniciar sesión
        private bool IniciarSesion()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("💗 INICIO DE SESIÓN 💗\n");
            Console.ResetColor();

            Console.Write("📧 Correo o usuario: ");
            string userInput = Console.ReadLine() ?? "";

            Console.Write("🔒 Contraseña: ");
            string password = LeerContraseniaOculta();

            var resultado = _repo.Login(userInput, password);

            if (!resultado.Exitoso)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ Usuario o contraseña incorrectos. Presione cualquier tecla para intentar de nuevo.");
                Console.ResetColor();
                Console.ReadKey();
                return false;
            }

            if (resultado.EsAdmin)
            {
                string connStr = "Host=localhost;Database=db_campuslove;Port=5432;Username=postgres;Password=root123";
                IDbFactory factory = new NpgsqlDbFactory(connStr);
                var adminUI = new AdminUI(factory);
                adminUI.MenuAdmin();
                return false;
            }


            else
            {

                var uiUsers = new UIUsers(
                    _userService,
                    _userInterestService,
                    _interestService,
                    _genderService,
                    _careerService,
                    _addressService,
                    _interactionsService,
                    _creditsService,
                    _matchesService,
                    _userStatisticsService,
                    _messagesService,
                    resultado.Usuario

                );

                uiUsers.Ejecutar();
                return true;
            }


        }
    }
}

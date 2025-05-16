using System;
using CampusLove.Application.Services;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;
using CampusLove.Infrastructure.Factories;
using CampusLove.Domain.Ports;

namespace CampusLove.Application.UI
{
    public class LoginUI
    {
        private readonly AuthService _repo;
        private readonly UserService _userService;
        private readonly GendersService _genderService;
        private readonly CareersService _careerService;
        private readonly AddressesService _addressService;
        private readonly InterestsService _interestService;
        private readonly UsersInterestsService _userInterestService;

        public LoginUI(
            AuthService repo,
            UserService userService,
            InterestsService interestService,
            UsersInterestsService userInterestService,
            GendersService genderService,
            CareersService careerService,
            AddressesService addressService)
        {
            _repo = repo;
            _userService = userService;
            _interestService = interestService;
            _userInterestService = userInterestService;
            _genderService = genderService;
            _careerService = careerService;
            _addressService = addressService;
        }

        // Título estilizado
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

        // Oculta la contraseña mientras se escribe
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

        // Menú principal del login
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
                        if (IniciarSesion())
                        {
                            Console.Clear();
                            MostrarMenuUsuario();
                        }
                        break;
                    case 2:
                        Console.Clear();
                        var creadorUsuario = new User.CreateUser(
                            _userService, _genderService, _careerService, _addressService);
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

        // Menú para usuarios ya autenticados
        private static void MostrarMenuUsuario()
        {
            bool volverMenuPrincipal = false;

            while (!volverMenuPrincipal)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n\t╔═══════════════════════════════════════╗");
                Console.WriteLine("\t║ 💗 BIENVENID A LA JERGA DEL AMOR 💗  ║");
                Console.WriteLine("\t╚═══════════════════════════════════════╝\n");
                Console.ResetColor();

                Console.WriteLine("1. 💗 Mis Likes");
                Console.WriteLine("2. 👀 Ver Perfiles");
                Console.WriteLine("3. 💌 Matches");
                Console.WriteLine("4. 📊 Ver Estadísticas");
                Console.WriteLine("0. 🚪 Salir\n");

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("💗 Seleccione una opción 💗 : ");
                Console.ResetColor();

                string? opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        // TODO: Implementar MostrarMisLikes();
                        break;
                    case "2":
                        // TODO: Implementar MostrarPerfiles();
                        break;
                    case "3":
                        // TODO: Implementar MostrarMatches();
                        break;
                    case "4":
                        // TODO: Implementar MostrarEstadisticas();
                        break;
                    case "0":
                        volverMenuPrincipal = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // Lógica para iniciar sesión
        private bool IniciarSesion()
        {
            Console.Write("\nIngrese su correo o usuario: ");
            string? identificador = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(identificador))
            {
                Console.WriteLine("⚠️ El identificador no puede estar vacío.");
                Console.ReadKey();
                return false;
            }

            Console.Write("\nIngrese su contraseña: ");
            string clave = LeerContraseniaOculta();

            var resultado = _repo.Login(identificador, clave);

            if (!resultado.Exitoso)
            {
                Console.WriteLine("❌ Usuario o contraseña incorrectos. Presione cualquier tecla para intentar de nuevo.");
                Console.ReadKey();
                return false;
            }

            if (resultado.EsAdmin)
            {
                string connStr = "Host=localhost;Database=db_campuslove;Port=5432;Username=postgres;password=root123;Pooling=true;";
                IDbFactory factory = new NpgsqlDbFactory(connStr);
                var adminUI = new AdminUI(factory);
                adminUI.MenuAdmin();
            }

            return true;
        }
    }
}

using CampusLove.Domain.Interfaces;

namespace CampusLove.Application.UI
{
    public class AdminUI
    {
        private readonly IDbFactory _factory;
        private IDbFactory factory;

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
                   "7. 🗺️ Manejo Región\n" +
                   "8. 👥 Manejo Género\n" +
                   "0. 🚪 Salir\n";
        }

        public void MenuAdmin()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("⋆｡ﾟ☁︎｡⋆｡ ﾟ☾ ﾟ｡⋆｡ﾟ☁︎｡⋆｡  ﾟ｡⋆｡⋆｡ ﾟ☾｡☁︎｡⋆｡ ﾟ☾☾｡⋆｡ ");
            Console.WriteLine("      💌 C A M P U S   L O V E 💌");
            Console.WriteLine("       ❝ BIENVENIDO ADMINISTRADOR ❞");
            Console.WriteLine("⋆｡ﾟ☁︎｡⋆｡ ﾟ☾ ﾟ｡⋆☁︎｡⋆｡ ﾟ☾｡ ﾟ☁︎｡⋆｡ ﾟ☾ ﾟ｡⋆｡ ﾟ｡ﾟ☁︎｡⋆\n");
            Console.ResetColor();

            bool salir = false;

            while (!salir)
            {
                Console.WriteLine(Menu());
                Console.Write("💗 Seleccione una opción 💗 : ");
                int opcion = Utilidades.LeerOpcionMenuKey(Menu());

                switch (opcion)
                {
                    case 1:
                        // TODO: Manejo Usuarios
                        break;
                    case 2:
                        // TODO: Manejo Carreras
                        break;
                    case 3:
                        var uiInterest = new UIInterest(_factory.CreateInterestsRepository());
                        uiInterest.Ejecutar();
                        break;
                    case 4:
                        // TODO: Manejo Dirección
                        break;
                    case 5:
                        // TODO: Manejo Ciudad
                        break;
                    case 6:
                        var uiCountry = new UICount(_factory.CreateCountryRepository());
                        uiCountry.GestionPaises();
                        break;
                    case 7:
                        // TODO: Manejo Región
                        break;
                    case 8:
                        // TODO: Manejo Género
                        break;
                    case 0:
                        Console.Write("\n¿Está seguro que desea salir? 🥺 (S/N): ");
                        salir = Utilidades.LeerTecla();
                        if (salir)
                        {
                            Console.Clear();
                            Console.WriteLine("\n👋 ¡Vuelve Pronto! 👋");
                        }
                        break;
                    default:
                        Console.WriteLine("⚠️ Opción no válida. ⚠️");
                        break;
                }
            }
        }
    }
}
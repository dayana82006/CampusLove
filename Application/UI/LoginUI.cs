using System;
using CampusLove.Application.Services;
using CampusLove.Domain.Entities;
using CampusLove;
using CampusLove.Domain.Ports;
namespace CampusLove.Application.UI;

using CampusLove.Application.UI;


public class LoginUI
{
    private readonly AuthService _repo;
    public LoginUI(AuthService repo)
    {
        _repo = repo;
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
        Console.WriteLine();
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
                contrasenia = contrasenia.Substring(0, contrasenia.Length - 1);
                Console.Write("\b \b");
            }
        } while (tecla.Key != ConsoleKey.Enter);

        Console.WriteLine();
        return contrasenia;
    }
    private static string MostrarOpciones()
    {
        return
               "1. Iniciar Sesion\n" +
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
            Console.WriteLine("💗 Seleccione una opcion 💗 : ");
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
                    Registrarse();
                    break;
                case 0:
                    Console.WriteLine("\n¿Está seguro que desea salir? 🥺 (S/N): ");
                    salir = Utilidades.LeerTecla();
                    Console.Clear();
                    Console.WriteLine("\n👋 Vuelve Pronto ! 👋");

                    break;
                default:
                    Console.WriteLine("⚠️ Opción no valida. ⚠️");
                    break;
            }
        }
    }


    static void MostrarMenuUsuario()
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
            Console.WriteLine("0. 🚪 Salir");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("💗 Seleccione una opcion 💗 : ");
            Console.ResetColor();

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    // MostrarMisLikes();
                    break;
                case "2":
                    //  MostrarPerfiles();
                    break;
                case "3":
                    // MostrarMatches();
                    break;
                case "4":
                    // MostrarEstadisticas();
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
    private bool IniciarSesion()
    {
        Console.Write("\nIngrese su correo o usuario: ");
        string identificador = Console.ReadLine()!;

        Console.Write("\nIngrese su contraseña: ");
        string clave = LeerContraseniaOculta();

        var resultado = _repo.Login(identificador, clave);

        if (!resultado.Exitoso) return false;

        if (resultado.EsAdmin)
        {
            AdminUI.MenuAdmin();
            return false;
        }

        return true;
    }


    private void Registrarse()
    {
        var nuevo = new Usuario();

        Console.Write("Nombre completo: ");
        nuevo.Nombre = Console.ReadLine()!;

        Console.Write("Correo electrónico: ");
        nuevo.Email = Console.ReadLine()!;

        Console.Write("Nombre de usuario: ");
        nuevo.UsuarioName = Console.ReadLine()!;

        Console.Write("Contraseña: ");
        string clave = LeerContraseniaOculta();
        nuevo.Clave = clave;

        Console.Write("Edad: ");
        nuevo.Edad = int.Parse(Console.ReadLine()!);

        Console.Write("ID Género: ");
        nuevo.GeneroId = int.Parse(Console.ReadLine()!);

        Console.Write("Selecciona el ID de tu carrera: ");
        nuevo.CarreraId = int.Parse(Console.ReadLine()!);

        Console.Write("ID Dirección: ");
        nuevo.DireccionId = int.Parse(Console.ReadLine()!);
        Console.Write("Selecciona Tus Interes");


        Console.Write("Frase de perfil: ");
        nuevo.FrasePerfil = Console.ReadLine()!;

        _repo.Registrar(nuevo);

    }


}

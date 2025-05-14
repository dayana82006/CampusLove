using System;
using CampusLove.Application.Services;
using CampusLove.Domain.Entities;
namespace CampusLove.Application.UI;


public class LoginUI
{
    private readonly AuthService _repo;
    public LoginUI(AuthService repo)
    {
        _repo = repo;
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

        Console.WriteLine(); // Salto de línea después de Enter
        return contrasenia;
    }
    public void MostrarMenu()
    {
        while (true)
        {
            Console.WriteLine("1. Iniciar sesión");
            Console.WriteLine("2. Registrarse");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine()!;

            switch (opcion)
            {
                case "1":
                    IniciarSesion();
                    break;
                case "2":
                    Registrarse();
                    break;
                case "0":
                    Console.WriteLine("👋 Hasta pronto.");
                    return;
                default:
                    Console.WriteLine("⚠️ Opción inválida.");
                    break;
            }
        }
    }

    private void IniciarSesion()
    {
        Console.Write("Ingrese su correo o usuario: ");
        string identificador = Console.ReadLine()!;

        Console.Write("Ingrese su contraseña: ");
        string clave = LeerContraseniaOculta();
        _repo.Login(identificador, clave);
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

        Console.Write("ID Carrera: ");
        nuevo.CarreraId = int.Parse(Console.ReadLine()!);

        Console.Write("ID Dirección: ");
        nuevo.DireccionId = int.Parse(Console.ReadLine()!);

        Console.Write("Frase de perfil: ");
        nuevo.FrasePerfil = Console.ReadLine()!;

        _repo.Registrar(nuevo);
    }


}

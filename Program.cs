using System;
using System.Threading;
using MySql.Data.MySqlClient;
using CampusLove.Domain.Factory;
using CampusLove.Infrastructure.MySql;
using CampusLove.Application.UI; 
using CampusLove.Application.Services;

internal class Program
{
    private static void MostrarBarraDeCarga()
    {
        Console.Write("Cargando: ");
        for (int i = 0; i <= 10; i++)
        {
            Console.Write("｡☁︎ ｡♥ ");
            Thread.Sleep(80);
        }
        Console.WriteLine("\n");
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

    private static void Main(string[] args)
    {
        MostrarBarraDeCarga();
        MostrarTitulo();

        try
        {
            var conexion = ConexionSingleton.Instancia.ObtenerConexion();
            var repo = new ImpUsuarioRepository(conexion); // Verifica que esté bien escrito (¿quizá quisiste decir UsuarioRepository?)
            var servicio = new AuthService(repo);
            var ui = new LoginUI(servicio);

            ui.MostrarMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Error al conectar a la base de datos: " + ex.Message);
        }
    }
}

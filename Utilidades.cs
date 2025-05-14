namespace CampusLove.Utilidades;

public class Utilidades
{
    public static bool LeerTecla()
    {
        while (true)
        {
            ConsoleKeyInfo tecla = Console.ReadKey(intercept: true);
            char opcion = char.ToUpper(tecla.KeyChar);
            switch (opcion)
            {
                case 'S':

                    return true;
                case 'N':
                    return false;
                default:
                    Console.Write("\nTecla no válida. Presione S o N: ");
                    break;
                    
            }
        }
    }

    public static int LeerOpcionMenuKey(string menu)
    {
        string opcionMenu = string.Empty;

        while (true)
        {
            ConsoleKeyInfo tecla = Console.ReadKey(intercept: true);


            if (tecla.Key == ConsoleKey.Enter)
            {
                if (!string.IsNullOrEmpty(opcionMenu))
                {
                    int opcion = int.Parse(opcionMenu);
                    return opcion;
                }
                continue;
            }


            if (tecla.Key == ConsoleKey.Backspace)
            {
                if (opcionMenu.Length > 0)
                {

                    opcionMenu = opcionMenu.Substring(0, opcionMenu.Length - 1);


                    Console.Write("\b \b");
                }
                continue;
            }

            if (char.IsDigit(tecla.KeyChar))
            {
                opcionMenu += tecla.KeyChar;
                Console.Write(tecla.KeyChar);
            }
            else
            {
                Console.Beep();
            }
        }
    }

    public static int LeerOpcionMenu(string menu)
    {
        while (true)
        {
            try
            {
                Console.Write("\nSeleccione una opción: ");
                string opcion = Console.ReadLine() ?? string.Empty;
                if (int.Parse(opcion) >= 1)
                {
                    return int.Parse(opcion);
                }
                else
                {
                    Console.Write("\nOpción no válida");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine(menu);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }

    public static int LeerEntero()
    {
        ConsoleKeyInfo tecla = Console.ReadKey(intercept: true);
        if (char.IsDigit(tecla.KeyChar))
        {
            Console.Write(tecla.KeyChar);
            return (int)char.GetNumericValue(tecla.KeyChar);
        }
        else
        {
            Console.Beep();
            return LeerEntero();
        }
    }

    public static string LeerTextoNoVacio(string mensaje)
    {
        string input;
        do
        {
            Console.Write(mensaje);
            input = Console.ReadLine()?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("El campo no puede estar vacío. Intente nuevamente.");
            }
        } while (string.IsNullOrWhiteSpace(input));

        return input;
    }

    public static int LeerEntero(string mensaje)
    {
        int valor;
        string input;
        do
        {
            input = LeerTextoNoVacio(mensaje);
            if (!int.TryParse(input, out valor))
            {
                Console.WriteLine("Debe ingresar un número entero válido.");
            }
        } while (!int.TryParse(input, out valor));

        return valor;
    }

    public static double LeerDouble(string mensaje)
    {
        double valor;
        string input;
        do
        {
            input = LeerTextoNoVacio(mensaje);
            if (!double.TryParse(input, out valor))
            {
                Console.WriteLine("Debe ingresar un número válido.");
            }
        } while (!double.TryParse(input, out valor));

        return valor;
    }

    public static DateTime LeerFecha(string mensaje)
    {
        DateTime valor;
        string input;
        do
        {
            input = LeerTextoNoVacio(mensaje + " (yyyy-MM-dd): ");
            if (!DateTime.TryParse(input, out valor))
            {
                Console.WriteLine("Formato de fecha inválido. Use yyyy-MM-dd.");
            }
        } while (!DateTime.TryParse(input, out valor));

        return valor;
    }

    public static bool LeerConfirmacion(string mensaje)
    {
        string respuesta;
        do
        {
            respuesta = LeerTextoNoVacio(mensaje + " (S/N): ").ToUpper();
            if (respuesta != "S" && respuesta != "N")
            {
                Console.WriteLine("Por favor ingrese S o N.");
            }
        } while (respuesta != "S" && respuesta != "N");

        return respuesta == "S";
    }

    public static void MostrarMenuInicio()
{
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
    Console.WriteLine("    💘  𝓑𝓘𝓔𝓝𝓥𝓔𝓝𝓘𝓓𝓞  𝓐  𝓛𝓐  𝓟𝓤𝓔𝓡𝓣𝓐  𝓓𝓔𝓛 𝓐𝓜𝓞𝓡 💘   ");
    Console.WriteLine("╚══════════════════════════════════════════════════════════╝");

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("    1. 💖 Mis Likes");
    Console.WriteLine("    2. 🧑‍❤️‍🧑 Ver Perfiles");
    Console.WriteLine("    3. 💌 Matches");
    Console.WriteLine("    4. 📊 Ver Estadísticas");
    Console.WriteLine("    0. 🚪 Salir");

    Console.ResetColor(); // Restablece color a blanco normal
}


}
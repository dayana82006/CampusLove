using System;
using System.Threading;
using MySql.Data.MySqlClient;

internal class Program
{
    private static void MostrarBarraDeCarga()
    {
        Console.Write("Cargando: ");
        for (int i = 0; i <= 20; i++)
        {
            Console.Write("■");
            Thread.Sleep(100);
        }
        Console.WriteLine("\n");
    }

    private static void Main(string[] args)
    {
        string connectionString = "server=localhost;database=campus_love;user=campus2023;password=campus2023;";
        bool conexionExitosa = false;

        MostrarBarraDeCarga();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("✅ Conexión exitosa a la base de datos.");
                conexionExitosa = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error al conectar a la base de datos: " + ex.Message);
            }
        }


    }
}
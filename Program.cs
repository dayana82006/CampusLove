using System;
using System.Threading;
using MySql.Data.MySqlClient;
using CampusLove.Domain.Factory;
using CampusLove.Infrastructure.MySql;
using CampusLove.Application.UI; 
using CampusLove.Application.Services;
using CampusLove.Utilidades;

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

   
    private static void Main(string[] args)
    {
        MostrarBarraDeCarga();
        try
        {
            var conexion = ConexionSingleton.Instancia.ObtenerConexion();
            var repo = new ImpUsuarioRepository(conexion); 
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

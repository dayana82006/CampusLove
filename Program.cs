﻿using System;
using System.Threading;
using MySql.Data.MySqlClient;
using CampusLove.Domain.Factory;
using CampusLove.Domain.Ports;
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
            Console.Write(" ☾｡☁︎♥");
            Thread.Sleep(100);
        }
        Console.WriteLine("\n");
    }

    private static void Main(string[] args)
    {
        string connectionString = "server=localhost;database=campus_love;user=root;password=root123;";
        bool conexionExitosa = false;

        MostrarBarraDeCarga();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

            // Crear la factory con la conexión
            IDbFactory factory = new MySqlDbFactory(connectionString);

            // Usar la factory para obtener el repositorio
            var usuarioRepo = factory.CrearUsuarioRepository();

            // Inyectar el repo al servicio
            var authService = new AuthService(usuarioRepo);

            // Crear la UI con el servicio y mostrar menú
            var loginUI = new LoginUI(authService);
            loginUI.MostrarMenu();
                conexionExitosa = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error al conectar a la base de datos: " + ex.Message);
            }
        }


    }
}
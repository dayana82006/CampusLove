﻿using System;
using MySql.Data.MySqlClient;
using CampusLove.Application.Services;
using CampusLove.Domain.Interfaces;
using CampusLove.Application.UI;
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

        string connStr = "Host=localhost;Database=db_campuslove;Port=5432;Username=postgres;password=root123;Pooling=true;";
        IDbFactory factory = new NpgsqlDbFactory(connStr);

        var userService = new UserService(factory.CreateUsersRepository());
        var genderService = new GendersService(factory.CreateGendersRepository());
        var careerService = new CareersService(factory.CreateCareersRepository());
        var addressService = new AddressesService(factory.CreateAddressesRepository());
        MostrarBarraDeCarga();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Usar la factory para obtener el repositorio
                var usuarioRepo = factory.CrearUsuarioRepository();

                // Inyectar el repo al servicio
                var authService = new AuthService(usuarioRepo);

                // Crear la UI con el servicio y mostrar menú
                var loginUI = new LoginUI(authService);
                loginUI.MostrarMenu();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error al conectar a la base de datos: " + ex.Message);
            }
        }


    }
}
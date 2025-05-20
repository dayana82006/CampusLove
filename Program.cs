﻿using System;
using System.Data;
using System.Threading;
using Npgsql;
using CampusLove.Application.Services;
using CampusLove.Domain.Interfaces;
using CampusLove.Application.UI;
using CampusLove.Infrastructure.Factories;

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

        MostrarBarraDeCarga();

        try
        {
            var genderRepo = factory.CreateGendersRepository();
            var careerRepo = factory.CreateCareersRepository();
            var addressRepo = factory.CreateAddressesRepository();
            var userRepo = factory.CreateUsersRepository();
            var interactionsCreditsRepo = factory.CreateInteractionCreditsRepository();
            var interactionsRepo = factory.CreateInteractionsRepository();
            var matchesRepo = factory.CreateMatchesRepository();
            var interestsRepo = factory.CreateInterestsRepository();
            var usersInterestsRepo = factory.CreateUsersInterestsRepository();
            var messagesRepo = factory.CreateMessagesRepository();
            var statisticsRepo = factory.CreateUserStatisticsRepository();

            var careersService = new CareersService(careerRepo);
            var addressesService = new AddressesService(addressRepo, connStr);
            var interestsService = new InterestsService(interestsRepo , factory.CreateCategoryRepository());
            var usersInterestsService = new UsersInterestsService(usersInterestsRepo);
            var authService = new AuthService(userRepo);
            var userService = new UserService(userRepo, interactionsCreditsRepo, interactionsRepo, matchesRepo);
            var genderService = new GendersService(genderRepo);

            var userStatisticsService = new UserStatisticsService(
                statisticsRepo,
                userRepo,
                interactionsRepo,
                matchesRepo
            );

            var interactionCreditsService = new InteractionCreditsService(interactionsRepo, interactionsCreditsRepo);

            var interactionsService = new InteractionsService(
                interactionsRepo,
                interactionCreditsService,
                userStatisticsService
            );

            var messagesService = new MessagesService(messagesRepo); 
            var matchesService = new MatchesService(matchesRepo, interactionsRepo, messagesService);
            messagesService.SetMatchesService(matchesService); 


            var loginUI = new LoginUI(
                authService,
                userService,
                interestsService,
                usersInterestsService,
                genderService,
                careersService,
                addressesService,
                interactionsService,
                interactionCreditsService,
                matchesService,
                messagesService,
                userStatisticsService
            );

            loginUI.MostrarMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Error al conectar a la base de datos: " + ex.Message);
        }
    }
}

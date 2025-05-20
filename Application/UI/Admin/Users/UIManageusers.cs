using System;
using CampusLove.Application.Services;
using CampusLove.Domain.Interfaces;
using CampusLove.Application.UI.User;

namespace CampusLove.Application.UI.Admin.Users
{
    public class UIManageusers
    {
        private readonly UserService _userService;
        private readonly GendersService _genderService;
        private readonly CareersService _careerService;
        private readonly AddressesService _addressService;
        private readonly InterestsService _interestService;
        private readonly UsersInterestsService _userInterestService;

public UIManageusers(
    IDbFactory factory,
    UserService userService,
    GendersService genderService,
    CareersService careerService,
    AddressesService addressService,
    InterestsService interestService,
    UsersInterestsService userInterestService)
{
    _userService = userService;
    _genderService = genderService;
    _careerService = careerService;
    _addressService = addressService;
    _interestService = interestService;
    _userInterestService = userInterestService;
}

        public void GestionUsers()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("             ⋆｡‧˚ʚ🍒ɞ˚‧｡⋆");
                Console.WriteLine("   ⭒❃.✮:▹ Menú de opciones ◃:✮.❃⭒\n");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" ⤜♡→ 1. Ver todos los usuarios 🌍");
                Console.WriteLine(" ⤜♡→ 2. Agregar un nuevo usuario ✨");
                Console.WriteLine(" ⤜♡→ 3. Actualizar información 📋");
                Console.WriteLine(" ⤜♡→ 4. Eliminar un usuario 💔");
                Console.WriteLine(" ⤜♡→ 0. Volver al menú principal ↩️");
                Console.ResetColor();

                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        _userService.ShowAll();
                        break;
                    case "2":
                        Console.Clear();
                        var creadorUsuario = new CreateUser(
                            _userService,
                            _genderService,
                            _careerService,
                            _addressService,
                            _interestService,
                            _userInterestService
                        );
                        creadorUsuario.Ejecutar();
                        break;
                    case "3":
                        var actualizar = new UpdateUser(_userService);
                        actualizar.Ejecutar();
                        break;
                    case "4":
                        var eliminar = new DeleteUser(_userService);
                        eliminar.Ejecutar();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("❌ Opción no válida.");
                        break;
                }
            }
        }
    }
}

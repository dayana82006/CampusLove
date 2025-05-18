using System;
using System.Linq;
using System.Collections.Generic;
using CampusLove.Domain.Entities;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.User
{
    public class ProfileViewer
    {
        private readonly UserService _userService;
        private readonly UsersInterestsService _usersInterestsService;
        private readonly InterestsService _interestsService;
        private readonly GendersService _gendersService;
        private readonly CareersService _careersService;
        private readonly AddressesService _addressesService;
        private readonly InteractionsService _interactionsService;
        private readonly InteractionCreditsService _creditsService;
        private readonly MatchesService _matchesService;
        private readonly dynamic _currentUser;

        public ProfileViewer(
            UserService userService,
            UsersInterestsService usersInterestsService,
            InterestsService interestsService,
            GendersService gendersService,
            CareersService careersService,
            AddressesService addressesService,
            InteractionsService interactionsService,
            InteractionCreditsService creditsService,
            MatchesService matchesService,
            dynamic currentUser)
        {
            _userService = userService;
            _usersInterestsService = usersInterestsService;
            _interestsService = interestsService;
            _gendersService = gendersService;
            _careersService = careersService;
            _addressesService = addressesService;
            _interactionsService = interactionsService;
            _creditsService = creditsService;
            _matchesService = matchesService;
            _currentUser = currentUser;
        }

        public void BrowseProfiles()
        {
            Console.Clear();
            var allUsers = _userService.ObtenerTodos();
            var otherUsers = allUsers.Where(u => u.id_user != _currentUser.id_user).ToList();

            if (!otherUsers.Any())
            {
                Console.WriteLine("❌ No hay perfiles para mostrar.");
                return;
            }

            int index = 0;
            _creditsService.CheckAndResetCredits(_currentUser.id_user); // Renueva créditos si es nuevo día

            while (true)
            {
                Console.Clear();
                var user = otherUsers[index];
                // Mostrar perfil actual y créditos disponibles
                ShowProfile(user);
                
                // Actualizar y mostrar créditos disponibles
                int availableCredits = _creditsService.GetAvailableCredits(_currentUser.id_user);
                Console.WriteLine($"\n💰 Créditos disponibles hoy: {availableCredits}/10");

                Console.WriteLine(@"
¿Qué deseas hacer?
    [L] Like
    [D] Dislike
    [N] Siguiente
    [P] Anterior
    [S] Salir
");
                var option = Console.ReadLine()?.Trim().ToUpper();

                switch (option)
                {
                    case "L":
                        {
                            try
                            {
                                bool creditWasDecremented = _interactionsService.RegisterInteraction(
                                    _currentUser.id_user, user.id_user, "like");

                                if (creditWasDecremented)
                                {
                                    availableCredits = _creditsService.GetAvailableCredits(_currentUser.id_user);
                                    Console.WriteLine($"💖 Diste like a {user.first_name}. Te quedan {availableCredits} créditos hoy.");

                                    if (_interactionsService.IsMutualLike(_currentUser.id_user, user.id_user))
                                    {
                                        bool matchCreated = _matchesService.CreateMatch(_currentUser.id_user, user.id_user);
                                        if (matchCreated)
                                        {
                                            Console.WriteLine("🎉 ¡Es un match! Ahora pueden comenzar a chatear.");
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"❌ Error al procesar like: {ex.Message}");
                            }
                        }
                        break;
                    case "D":
                        {
                            try
                            {
                                _interactionsService.RegisterInteraction(_currentUser.id_user, user.id_user, "dislike");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"❌ Error al procesar dislike: {ex.Message}");
                            }
                        }
                        break;

                    case "N":
                        index = (index + 1) % otherUsers.Count;
                        break;

                    case "P":
                        index = (index - 1 + otherUsers.Count) % otherUsers.Count;
                        break;

                    case "S":
                        return;

                    default:
                        Console.WriteLine("⚠️ Opción no válida.");
                        break;
                }

                if (option != "N" && option != "P")
                {
                    Console.WriteLine("Presiona una tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        private void ShowProfile(Users user)
        {
            var gender = _gendersService.GetById(user.id_gender)?.genre_name ?? "No especificado";
            var career = _careersService.GetById(user.id_career)?.career_name ?? "No especificado";
            var address = _addressesService.GetFullAddress(user.id_address);
            var userInterests = (IEnumerable<UsersInterests>)_usersInterestsService.GetUserInterests(user.id_user);
            var interests = userInterests.Select(ui => _interestsService.GetById(ui.id_interest)?.interest_name)
                                         .Where(i => i != null);

            string interestsList = string.Join(Environment.NewLine, interests.Select(i => "                        - " + i));

            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();
            Console.WriteLine($@"
╔══════════════════════════════════════════════════════════════════╗
║     ♥♥♥ BIENVENID@ A TU PERFIL ROMÁNTICO PERSONALIZADO ♥♥♥     ║
╚══════════════════════════════════════════════════════════════════╝

👤 Nombre        : {user.first_name} {user.last_name}
🎂 Edad          : {CalculateAge(user.birth_date)} años
📧 Email         : {user.email}
🚻 Género        : {gender}
🎓 Carrera       : {career}
💬 Frase de perfil:
❝ {user.profile_phrase} ❞
🏠 Ubicación     : {address}

⊹⊱✿⊰⊹ Intereses del Corazón ⊹⊱✿⊰⊹

    {interestsList}

˗ˏˋ ★ ˎˊ˗ ʕ•́ᴥ•̀ʔっ♡ Gracias por ser parte de este viaje amoroso.

");
        }

        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
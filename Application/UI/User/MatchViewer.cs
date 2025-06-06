using System;
using System.Collections.Generic;
using System.Linq;
using CampusLove.Domain.Entities;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.User
{
    public class MatchViewer
    {
        private readonly UserService _userService;
        private readonly MatchesService _matchesService;
        private readonly InteractionsService _interactionsService;
        private readonly UsersInterestsService _usersInterestsService;
        private readonly InterestsService _interestsService;
        private readonly GendersService _gendersService;
        private readonly CareersService _careersService;
        private readonly AddressesService _addressesService;
        private readonly MessagesService _messagesService;

        private readonly dynamic _currentUser;

        public MatchViewer(
            UserService userService,
            MatchesService matchesService,
            InteractionsService interactionsService,
            UsersInterestsService usersInterestsService,
            InterestsService interestsService,
            GendersService gendersService,
            CareersService careersService,
            AddressesService addressesService,
            MessagesService messagesService,
            dynamic currentUser)
        {
            _userService = userService;
            _matchesService = matchesService;
            _interactionsService = interactionsService;
            _usersInterestsService = usersInterestsService;
            _interestsService = interestsService;
            _gendersService = gendersService;
            _careersService = careersService;
            _addressesService = addressesService;
            _currentUser = currentUser;
            _messagesService = messagesService;
        }

        public void DisplayMatches()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\t ♥ ¸.•*¨*•♫♪♥ MIS COINCIDENCIAS ♥ ♪♫•*¨*•.¸ ♥");
            Console.ResetColor();

            List<Users> matchedUsers = GetMatchedUsers((int)_currentUser.id_user);

            if (!matchedUsers.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n\t🔍 Aún no tienes coincidencias. ¡Sigue dando likes!");
                Console.WriteLine("\t💫 ¡Las mejores historias de amor comienzan con un simple like!");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n\t✨ ¡Felicidades! Has conectado con {matchedUsers.Count} persona(s)");
            Console.ResetColor();

            int index = 0;
            while (true)
            {
                if (matchedUsers.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n\t🔍 Ya no tienes coincidencias activas.");
                    Console.ResetColor();
                    break;
                }

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\t ♥ ¸.•*¨*•♫♪♥ MIS COINCIDENCIAS ♥ ♪♫•*¨*•.¸ ♥");
                Console.ResetColor();

                Users matchedUser = matchedUsers[index];
                ShowMatchedUserProfile(matchedUser);

                DateTime matchDate = GetMatchDate((int)_currentUser.id_user, matchedUser.id_user);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n\t🔢 Mostrando coincidencia {index + 1} de {matchedUsers.Count}");
                Console.WriteLine($"\t📅 Fecha del match: {matchDate.ToString("dd/MM/yyyy HH:mm")}");
                Console.ResetColor();

                Console.WriteLine(@"
¿Qué deseas hacer?
    [N] Siguiente
    [P] Anterior
    [M] Ver/enviar mensajes
    [D] Eliminar coincidencia (cambiar a dislike)
    [S] Salir
");
                var option = Console.ReadLine()?.Trim().ToUpper();

                switch (option)
                {
                    case "N":
                        index = (index + 1) % matchedUsers.Count;
                        break;

                    case "P":
                        index = (index - 1 + matchedUsers.Count) % matchedUsers.Count;
                        break;

                    case "D":

                        Console.WriteLine($"¿Estás seguro que deseas eliminar la coincidencia con {matchedUser.first_name}? (S/N)");
                        var confirm = Console.ReadLine()?.Trim().ToUpper();

                        if (confirm == "S")
                        {
                            try
                            {
                                _interactionsService.RegisterInteraction((int)_currentUser.id_user, matchedUser.id_user, "dislike");
                                Console.WriteLine($"💔 Has eliminado la coincidencia con {matchedUser.first_name}.");

                                matchedUsers = GetMatchedUsers((int)_currentUser.id_user);

                                if (matchedUsers.Count == 0)
                                {
                                    Console.WriteLine("Ya no tienes coincidencias. ¡Sigue explorando!");
                                    Console.WriteLine("Presiona cualquier tecla para volver al menú principal...");
                                    Console.ReadKey();
                                    return;
                                }

                                if (index >= matchedUsers.Count)
                                {
                                    index = matchedUsers.Count - 1;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"❌ Error al eliminar coincidencia: {ex.Message}");
                            }
                        }
                        break;
                    case "M":
                        _messagesService.VisualizarMensajes((int)_currentUser.id_user, matchedUser.id_user);
                        break;
                    case "S":
                        return;

                    default:
                        Console.WriteLine("⚠️ Opción no válida.");
                        break;
                }
            }
        }

        private DateTime GetMatchDate(int userId1, int userId2)
        {
            var allMatches = _matchesService.GetAllMatches();
            var (minId, maxId) = userId1 < userId2 ? (userId1, userId2) : (userId2, userId1);

            var match = allMatches.FirstOrDefault(m => m.id_user1 == minId && m.id_user2 == maxId);
            return match?.match_date ?? DateTime.Now;
        }

        private List<Users> GetMatchedUsers(int currentUserId)
        {
            List<Users> matchedUsers = new List<Users>();
            var allUsers = _userService.ObtenerTodos();

            foreach (var user in allUsers)
            {
                if (user.id_user != currentUserId && _matchesService.IsMatch(currentUserId, user.id_user))
                {
                    if (_interactionsService.IsMutualLike(currentUserId, user.id_user))
                    {
                        matchedUsers.Add(user);
                    }
                }
            }

            return matchedUsers;
        }

        private void ShowMatchedUserProfile(Users user)
        {
            var gender = _gendersService.GetById(user.id_gender)?.genre_name ?? "No especificado";
            var career = _careersService.GetById(user.id_career)?.career_name ?? "No especificado";
            var address = _addressesService.GetFullAddress(user.id_address);
            var userInterests = (IEnumerable<UsersInterests>)_usersInterestsService.GetUserInterests(user.id_user);
            var interests = userInterests.Select(ui => _interestsService.GetById(ui.id_interest)?.interest_name)
                                         .Where(i => i != null);

            string interestsList = string.Join(Environment.NewLine, interests.Select(i => "                        - " + i));

            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($@"
╔══════════════════════════════════════════════════════════════════╗
║        ♥♥♥ ¡MATCH! ESTA PERSONA TAMBIÉN TE DIO LIKE ♥♥♥        ║
╚══════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($@"
👤 Nombre        : {user.first_name} {user.last_name}
🎂 Edad          : {CalculateAge(user.birth_date)} años
📧 Email         : {user.email}
🚻 Género        : {gender}
🎓 Carrera       : {career}
💬 Frase de perfil:
❝ {user.profile_phrase} ❞
🏠 Ubicación     : {address}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($@"
⊹⊱✿⊰⊹ Intereses en común ⊹⊱✿⊰⊹

{interestsList}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($@"
˗ˏˋ ★ ˎˊ˗ ʕ•́ᴥ•̀ʔっ♡ ¡Una nueva aventura romántica puede comenzar!
");
            Console.ResetColor();
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
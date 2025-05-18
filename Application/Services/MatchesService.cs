using System;
using System.Collections.Generic;
using System.Linq;
using CampusLove.Domain.Interfaces;
using CampusLove.Domain.Entities;

namespace CampusLove.Application.Services
{
    public class MatchesService
    {
        private readonly IMatchesRepository _matchesRepository;
        private readonly IInteractionsRepository _interactionsRepository;

        public MatchesService(
            IMatchesRepository matchesRepository,
            IInteractionsRepository interactionsRepository)
        {
            _matchesRepository = matchesRepository;
            _interactionsRepository = interactionsRepository;
        }

        public bool CreateMatch(int userId1, int userId2)
        {
            // Verificar primero si ambos usuarios se dieron like mutuamente
            bool user1LikedUser2 = _interactionsRepository.GetAll()
                .Any(i => i.id_user_origin == userId1 && 
                          i.id_user_target == userId2 && 
                          i.interaction_type == "like");

            bool user2LikedUser1 = _interactionsRepository.GetAll()
                .Any(i => i.id_user_origin == userId2 && 
                          i.id_user_target == userId1 && 
                          i.interaction_type == "like");

            if (!user1LikedUser2 || !user2LikedUser1)
            {
                return false; // No hay likes mutuos, no se crea match
            }

            // Ordenar IDs para mantener consistencia en cómo se guardan los matches
            var (first, second) = userId1 < userId2 ? (userId1, userId2) : (userId2, userId1);

            // Verificar si ya existe un match
            if (_matchesRepository.MatchExists(first, second))
            {
                return true; // El match ya existe
            }

            // Crear nuevo match
            var match = new Matches
            {
                id_user1 = first,
                id_user2 = second,
                match_date = DateTime.Now
            };

            _matchesRepository.Insert(match);
            
            Console.WriteLine($"🎉 ¡Match creado entre los usuarios {first} y {second}!");
            return true;
        }

        public bool IsMatch(int userId1, int userId2)
        {
            // Verificar si existe el registro en la base de datos
            var (first, second) = userId1 < userId2 ? (userId1, userId2) : (userId2, userId1);
            bool matchExists = _matchesRepository.MatchExists(first, second);
            
            if (!matchExists)
            {
                return false;
            }
            
            // Verificar si ambos usuarios aún tienen likes mutuos
            bool user1LikedUser2 = _interactionsRepository.GetAll()
                .Any(i => i.id_user_origin == userId1 && 
                          i.id_user_target == userId2 && 
                          i.interaction_type == "like");

            bool user2LikedUser1 = _interactionsRepository.GetAll()
                .Any(i => i.id_user_origin == userId2 && 
                          i.id_user_target == userId1 && 
                          i.interaction_type == "like");

            // Solo se considera match si ambos siguen teniendo likes mutuos
            return user1LikedUser2 && user2LikedUser1;
        }
        
        public List<Users> GetUserMatches(int userId, IEnumerable<Users> allUsers)
        {
            List<Users> matches = new List<Users>();
            
            foreach (var user in allUsers)
            {
                if (user.id_user != userId && IsMatch(userId, user.id_user))
                {
                    matches.Add(user);
                }
            }
            
            return matches;
        }
    }
}
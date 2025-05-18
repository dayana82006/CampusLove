using System;
using System.Collections.Generic;
using System.Linq;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Application.Services
{
    public class UserStatisticsService
    {
        private readonly IUserStatisticsRepository _userStatisticsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IInteractionsRepository _interactionsRepository;
        private readonly IMatchesRepository _matchesRepository;

        public UserStatisticsService(
            IUserStatisticsRepository userStatisticsRepository,
            IUsersRepository usersRepository,
            IInteractionsRepository interactionsRepository,
            IMatchesRepository matchesRepository)
        {
            _userStatisticsRepository = userStatisticsRepository;
            _usersRepository = usersRepository;
            _interactionsRepository = interactionsRepository;
            _matchesRepository = matchesRepository;
        }

        public UserStatistics GetUserStatistics(int userId)
        {
            try
            {
                var stats = _userStatisticsRepository.GetByUserId(userId);

                if (stats == null)
                {
                    stats = CreateAndInitializeStatistics(userId);
                }
                else
                {
                    UpdateUserStatistics(userId);
                    stats = _userStatisticsRepository.GetByUserId(userId);
                }

                return stats;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener estadísticas del usuario: {ex.Message}");
                return new UserStatistics { id_user = userId };
            }
        }

        private UserStatistics CreateAndInitializeStatistics(int userId)
        {
            try
            {
                var stats = new UserStatistics
                {
                    id_user = userId,
                    received_likes = 0,
                    received_dislikes = 0,
                    sent_likes = 0,
                    sent_dislikes = 0,
                    total_matches = 0,
                    last_update = DateTime.Now
                };

                CalculateUserStatistics(userId, stats);
                
                _userStatisticsRepository.Add(stats);
                
                return stats;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al crear estadísticas del usuario: {ex.Message}");
                return new UserStatistics { id_user = userId };
            }
        }

        // Actualizar las estadísticas de un usuario
        public void UpdateUserStatistics(int userId)
        {
            try
            {
                var stats = _userStatisticsRepository.GetByUserId(userId);
                
                if (stats == null)
                {
                    CreateAndInitializeStatistics(userId);
                    return;
                }

                CalculateUserStatistics(userId, stats);
                
                stats.last_update = DateTime.Now;
                
                _userStatisticsRepository.Update(stats);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al actualizar estadísticas del usuario: {ex.Message}");
            }
        }

        private void CalculateUserStatistics(int userId, UserStatistics stats)
        {
            var interactions = _interactionsRepository.GetAll();
            
            // Contar likes y dislikes enviados
            stats.sent_likes = interactions.Count(i => i.id_user_origin == userId && i.interaction_type == "like");
            stats.sent_dislikes = interactions.Count(i => i.id_user_origin == userId && i.interaction_type == "dislike");
            
            // Contar likes y dislikes recibidos
            stats.received_likes = interactions.Count(i => i.id_user_target == userId && i.interaction_type == "like");
            stats.received_dislikes = interactions.Count(i => i.id_user_target == userId && i.interaction_type == "dislike");
            
            // Contar matches
            var matches = _matchesRepository.GetAllMatches();
            stats.total_matches = matches.Count(m => m.id_user1 == userId || m.id_user2 == userId);
        }

        // Registrar like enviado
        public void RegisterSentLike(int userId)
        {
            var stats = GetUserStatistics(userId);
            stats.sent_likes++;
            stats.last_update = DateTime.Now;
            _userStatisticsRepository.Update(stats);
        }

        // Registrar dislike enviado
        public void RegisterSentDislike(int userId)
        {
            var stats = GetUserStatistics(userId);
            stats.sent_dislikes++;
            stats.last_update = DateTime.Now;
            _userStatisticsRepository.Update(stats);
        }

        // Registrar like recibido
        public void RegisterReceivedLike(int userId)
        {
            var stats = GetUserStatistics(userId);
            stats.received_likes++;
            stats.last_update = DateTime.Now;
            _userStatisticsRepository.Update(stats);
        }

        // Registrar dislike recibido
        public void RegisterReceivedDislike(int userId)
        {
            var stats = GetUserStatistics(userId);
            stats.received_dislikes++;
            stats.last_update = DateTime.Now;
            _userStatisticsRepository.Update(stats);
        }

        // Registrar nuevo match
        public void RegisterNewMatch(int userId)
        {
            var stats = GetUserStatistics(userId);
            stats.total_matches++;
            stats.last_update = DateTime.Now;
            _userStatisticsRepository.Update(stats);
        }

        // Obtener usuarios con más likes recibidos
        public List<(Users User, int Likes)> GetMostLikedUsers(int topCount = 5)
        {
            try
            {
                var allUsers = _usersRepository.GetAll();
                var result = new List<(Users User, int Likes)>();

                foreach (var user in allUsers)
                {
                    var stats = GetUserStatistics(user.id_user);
                    result.Add((user, stats.received_likes));
                }

                return result.OrderByDescending(item => item.Likes).Take(topCount).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener usuarios más populares: {ex.Message}");
                return new List<(Users User, int Likes)>();
            }
        }

        // Obtener usuarios con más matches
        public List<(Users User, int Matches)> GetUsersWithMostMatches(int topCount = 5)
        {
            try
            {
                var allUsers = _usersRepository.GetAll();
                var result = new List<(Users User, int Matches)>();

                foreach (var user in allUsers)
                {
                    var stats = GetUserStatistics(user.id_user);
                    result.Add((user, stats.total_matches));
                }

                return result.OrderByDescending(item => item.Matches).Take(topCount).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener usuarios con más matches: {ex.Message}");
                return new List<(Users User, int Matches)>();
            }
        }

        // Obtener usuarios más activos 
        public List<(Users User, int SentLikes)> GetMostActiveUsers(int topCount = 5)
        {
            try
            {
                var allUsers = _usersRepository.GetAll();
                var result = new List<(Users User, int SentLikes)>();

                foreach (var user in allUsers)
                {
                    var stats = GetUserStatistics(user.id_user);
                    result.Add((user, stats.sent_likes));
                }

                return result.OrderByDescending(item => item.SentLikes).Take(topCount).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al obtener usuarios más activos: {ex.Message}");
                return new List<(Users User, int SentLikes)>();
            }
        }

        public List<(Users User, double Ratio)> GetUsersWithBestLikeRatio(int topCount = 5)
{
    try
    {
        var allUsers = _usersRepository.GetAll();
        var result = new List<(Users User, double Ratio)>();

        foreach (var user in allUsers)
        {
            var stats = GetUserStatistics(user.id_user);
            double ratio = 0;

            int totalReceived = stats.received_likes + stats.received_dislikes;

            if (totalReceived > 0)
            {
                ratio = (double)stats.received_likes / totalReceived;
                result.Add((user, ratio)); 
            }
        }

        return result
            .OrderByDescending(item => item.Ratio)
            .Take(topCount)
            .ToList();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error al obtener usuarios con mejor ratio: {ex.Message}");
        return new List<(Users User, double Ratio)>();
    }
}

    }
}
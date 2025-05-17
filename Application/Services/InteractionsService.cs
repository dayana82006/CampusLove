using System;
using System.Linq;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Application.Services
{
    public class InteractionsService
    {
        private readonly IInteractionsRepository _interactionsRepository;
        private const int DailyCreditLimit = 5; 

        public InteractionsService(IInteractionsRepository interactionsRepository)
        {
            _interactionsRepository = interactionsRepository;
        }

        public void RegisterInteraction(int userId, int targetUserId, string interactionType)
        {
            var existingInteraction = _interactionsRepository
                .GetAll()
                .FirstOrDefault(i => i.id_user_origin == userId && i.id_user_target == targetUserId);

            if (existingInteraction != null)
            {
                if (existingInteraction.interaction_type == "like" && interactionType == "like")
                {
                    Console.WriteLine("⚠️ Ya le diste like a este usuario. No se descontarán créditos.");
                    return; 
                }
                else if (interactionType == "dislike")
                {
                    existingInteraction.interaction_type = "dislike";
                    existingInteraction.interaction_date = DateTime.Today;
                    _interactionsRepository.Update(existingInteraction);
                    Console.WriteLine("🔁 Has cambiado tu interacción a 'dislike'.");
                    return;
                }
                else
                {
                    Console.WriteLine("⚠️ Ya existe una interacción con este usuario.");
                    return;
                }
            }

            var interaction = new Interactions
            {
                id_user_origin = userId,
                id_user_target = targetUserId,
                interaction_type = interactionType,
                interaction_date = DateTime.Today
            };

            _interactionsRepository.Add(interaction);
        }

        public int GetRemainingCredits(int userId)
        {
            var today = DateTime.Today;
            var usedToday = _interactionsRepository
                .GetAll()
                .Where(i => i.id_user_origin == userId && i.interaction_date.Date == today)
                .Count();

            return Math.Max(DailyCreditLimit - usedToday, 0);
        }

        public string ShowRemainingCredits(int userId)
        {
            int remaining = GetRemainingCredits(userId);
            return $"💰 Créditos disponibles hoy: {remaining}";
        }

        public bool IsMutualLike(int userId1, int userId2)
        {
            var allInteractions = _interactionsRepository.GetAll();

            bool user1LikedUser2 = allInteractions.Any(i =>
                i.id_user_origin == userId1 &&
                i.id_user_target == userId2 &&
                i.interaction_type == "like");

            bool user2LikedUser1 = allInteractions.Any(i =>
                i.id_user_origin == userId2 &&
                i.id_user_target == userId1 &&
                i.interaction_type == "like");

            return user1LikedUser2 && user2LikedUser1;
        }
    }
}

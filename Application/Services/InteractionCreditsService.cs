using System;
using System.Collections.Generic;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Application.Services
{
    public class InteractionCreditsService
    {
        private readonly IInteractionsRepository _interactionsRepository;
        private readonly IInteractionsCreditsRepository _interactionCreditsRepository;
        private const int MaxDailyCredits = 10;

        public InteractionCreditsService(
            IInteractionsRepository interactionsRepository,
            IInteractionsCreditsRepository interactionCreditsRepository)
        {
            _interactionsRepository = interactionsRepository;
            _interactionCreditsRepository = interactionCreditsRepository;
        }

        public void CheckAndResetCredits(int userId)
        {
            var credits = _interactionCreditsRepository.GetByUserId(userId);

            if (credits == null)
            {
                // Primera vez del usuario, crear registro de créditos
                var newCredits = new InteractionCredits
                {
                    id_user = userId,
                    available_credits = MaxDailyCredits,
                    last_update_date = DateTime.UtcNow.Date
                };
                _interactionCreditsRepository.Add(newCredits);
                return;
            }

            // Si es un nuevo día, reiniciar créditos
            if (credits.last_update_date.Date < DateTime.UtcNow.Date)
            {
                credits.available_credits = MaxDailyCredits;
                credits.last_update_date = DateTime.UtcNow.Date;
                _interactionCreditsRepository.Update(credits);
                Console.WriteLine("🎁 ¡Tus créditos han sido renovados para hoy!");
            }
        }

        public int GetAvailableCredits(int userId)
        {
            var credits = _interactionCreditsRepository.GetByUserId(userId);
            return credits?.available_credits ?? 0;
        }

        public void DecrementCredit(int userId)
        {
            var credits = _interactionCreditsRepository.GetByUserId(userId);
            if (credits != null && credits.available_credits > 0)
            {
                credits.available_credits--;
                _interactionCreditsRepository.Update(credits);
            }
        }

        public int GetInteractionCredits(int userId)
        {
            return _interactionCreditsRepository.GetCreditsByUserId(userId);
        }

        public IEnumerable<Interactions> GetUserInteractions(int userId)
        {
            return _interactionsRepository.GetByUserId(userId);
        }
    }
}

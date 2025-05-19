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
            try {
                var credits = _interactionCreditsRepository.GetByUserId(userId);

                if (credits == null)
                {
                    // Primera vez del usuario, crear registro de créditos
                    var newCredits = new InteractionCredits
                    {
                        id_user = userId,
                        available_credits = MaxDailyCredits,
                        last_update_date = DateTime.Today
                    };
                    _interactionCreditsRepository.Add(newCredits);
                    return;
                }

                // Si es un nuevo día, reiniciar créditos
                if (credits.last_update_date.Date < DateTime.Today)
                {
                    credits.available_credits = MaxDailyCredits;
                    credits.last_update_date = DateTime.Today;
                    _interactionCreditsRepository.Update(credits);
                    Console.WriteLine("🎁 ¡Tus créditos han sido renovados para hoy!");
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"❌ Error al verificar créditos: {ex.Message}");
            }
        }

        public int GetAvailableCredits(int userId)
        {
            try {
                var credits = _interactionCreditsRepository.GetByUserId(userId);
                return credits?.available_credits ?? 0;
            }
            catch (Exception) {
                // Si hay un error, devolver 0 para prevenir likes no autorizados
                return 0;
            }
        }

        public void DecrementCredit(int userId)
        {
            try {
                var credits = _interactionCreditsRepository.GetByUserId(userId);
                if (credits != null && credits.available_credits > 0)
                {
                    credits.available_credits--;
                    _interactionCreditsRepository.Update(credits);
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"❌ Error al decrementar crédito: {ex.Message}");
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
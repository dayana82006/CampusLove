using System;
using System.Linq;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Application.Services
{
    public class InteractionsService
    {
        private readonly IInteractionsRepository _interactionsRepository;
        private readonly InteractionCreditsService _creditsService;

        public InteractionsService(IInteractionsRepository interactionsRepository, InteractionCreditsService creditsService)
        {
            _interactionsRepository = interactionsRepository;
            _creditsService = creditsService;
        }

        public bool RegisterInteraction(int userId, int targetUserId, string interactionType)
        {
            try
            {
                _creditsService.CheckAndResetCredits(userId);

                var existingInteraction = _interactionsRepository
                    .GetAll()
                    .FirstOrDefault(i => i.id_user_origin == userId && i.id_user_target == targetUserId);

                if (interactionType == "like")
                {
                  
                    bool needsCredit = existingInteraction == null || 
                                      (existingInteraction != null && existingInteraction.interaction_type == "dislike");
                    
                    if (needsCredit)
                    {
                        int availableCredits = _creditsService.GetAvailableCredits(userId);
                        if (availableCredits <= 0)
                        {
                            Console.WriteLine("⚠️ No te quedan créditos para dar likes hoy. Regresa mañana.");
                            return false;
                        }
                    }
                }

                if (existingInteraction != null)
                {
                    // Ya existe una interacción previa, verificar si es del mismo tipo
                    if (existingInteraction.interaction_type == interactionType)
                    {
                        // Mismo tipo - no hacer nada
                        Console.WriteLine($"⚠️ Ya diste {interactionType} a este usuario. No se modifican créditos.");
                        return false;
                    }

                    // Cambio de tipo de interacción
                    if (interactionType == "like")
                    {
                        // Cambio de dislike a like: se descuenta crédito
                        existingInteraction.interaction_type = "like";
                        existingInteraction.interaction_date = DateTime.Today; 
                        _interactionsRepository.Update(existingInteraction);
                        _creditsService.DecrementCredit(userId);

                        Console.WriteLine("👍 Cambiaste de dislike a like. Crédito descontado.");
                        return true;
                    }
                    else if (interactionType == "dislike")
                    {
                        // Cambio de like a dislike: no se afectan créditos
                        existingInteraction.interaction_type = "dislike";
                        existingInteraction.interaction_date = DateTime.Today; 
                        _interactionsRepository.Update(existingInteraction);

                        Console.WriteLine("👎 Cambiaste de like a dislike. No se devuelven créditos.");
                        return false;
                    }
                }
                else
                {
                    // No existía interacción previa
                    var newInteraction = new Interactions
                    {
                        id_user_origin = userId,
                        id_user_target = targetUserId,
                        interaction_type = interactionType,
                        interaction_date = DateTime.Today
                    };
                    
                    if (interactionType == "like")
                    {
                        // Nuevo like: descontar crédito
                        _interactionsRepository.Add(newInteraction);
                        _creditsService.DecrementCredit(userId);

                        Console.WriteLine("👍 Like registrado. Crédito descontado.");
                        return true;
                    }
                    else if (interactionType == "dislike")
                    {
                        _interactionsRepository.Add(newInteraction);
                        Console.WriteLine("👎 Dislike registrado. No se descuentan créditos.");
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al registrar interacción: {ex.Message}");
                return false;
            }
        }

        public bool IsMutualLike(int userId, int targetUserId)
        {
            var interactionFromCurrent = _interactionsRepository
                .GetAll()
                .FirstOrDefault(i => i.id_user_origin == userId
                                  && i.id_user_target == targetUserId
                                  && i.interaction_type == "like");

            var interactionFromTarget = _interactionsRepository
                .GetAll()
                .FirstOrDefault(i => i.id_user_origin == targetUserId
                                  && i.id_user_target == userId
                                  && i.interaction_type == "like");

            return interactionFromCurrent != null && interactionFromTarget != null;
        }
    }
}
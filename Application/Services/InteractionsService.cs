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
                // Siempre revisamos si hay que resetear los créditos
                _creditsService.CheckAndResetCredits(userId);

                // Buscar interacción existente por usuario origen, usuario destino (sin filtrar por fecha)
                var existingInteraction = _interactionsRepository
                    .GetAll()
                    .FirstOrDefault(i => i.id_user_origin == userId && i.id_user_target == targetUserId);

                if (existingInteraction != null)
                {
                    // Ya existe una interacción previa
                    if (existingInteraction.interaction_type == interactionType)
                    {
                        // Mismo tipo - no hacer nada
                        Console.WriteLine($"⚠️ Ya diste {interactionType} a este usuario anteriormente. No se modifican créditos.");
                        return false;
                    }

                    // Cambio de tipo de interacción
                    if (interactionType == "like")
                    {
                        // Verificamos créditos disponibles para cambiar a like
                        int availableCredits = _creditsService.GetAvailableCredits(userId);
                        if (availableCredits <= 0)
                        {
                            Console.WriteLine("⚠️ No te quedan créditos para dar likes hoy.");
                            return false;
                        }

                        // Actualizar la interacción existente
                        existingInteraction.interaction_type = "like";
                        existingInteraction.interaction_date = DateTime.Today; // Actualizar fecha
                        _interactionsRepository.Update(existingInteraction);
                        _creditsService.DecrementCredit(userId);

                        Console.WriteLine("👍 Cambiaste de dislike a like. Crédito descontado.");
                        return true;
                    }
                    else if (interactionType == "dislike")
                    {
                        // Cambiar de like a dislike
                        existingInteraction.interaction_type = "dislike";
                        existingInteraction.interaction_date = DateTime.Today; // Actualizar fecha
                        _interactionsRepository.Update(existingInteraction);

                        Console.WriteLine("👎 Cambiaste de like a dislike. No se modifican créditos.");
                        return false;
                    }
                }
                else
                {
                    // No existía interacción previa
                    if (interactionType == "like")
                    {
                        int availableCredits = _creditsService.GetAvailableCredits(userId);
                        if (availableCredits <= 0)
                        {
                            Console.WriteLine("⚠️ No te quedan créditos para dar likes hoy.");
                            return false;
                        }

                        // Crear nueva interacción
                        var newInteraction = new Interactions
                        {
                            id_user_origin = userId,
                            id_user_target = targetUserId,
                            interaction_type = "like",
                            interaction_date = DateTime.Today
                        };
                        
                        _interactionsRepository.Add(newInteraction);
                        _creditsService.DecrementCredit(userId);

                        Console.WriteLine("👍 Like registrado. Crédito descontado.");
                        return true;
                    }
                    else if (interactionType == "dislike")
                    {
                        // Crear nueva interacción de dislike
                        var newInteraction = new Interactions
                        {
                            id_user_origin = userId,
                            id_user_target = targetUserId,
                            interaction_type = "dislike",
                            interaction_date = DateTime.Today
                        };
                        
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
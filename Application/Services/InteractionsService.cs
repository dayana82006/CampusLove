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

                // Si la interacción es un like, primero verificar si hay créditos suficientes
                if (interactionType == "like")
                {
                    // Solo verificar créditos si:
                    // 1. No existe interacción previa, o
                    // 2. Existe una interacción pero es de tipo dislike (cambio de dislike a like)
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

                // Proceso según si existe o no una interacción previa
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
                        existingInteraction.interaction_date = DateTime.Today; // Actualizar fecha
                        _interactionsRepository.Update(existingInteraction);
                        _creditsService.DecrementCredit(userId);

                        Console.WriteLine("👍 Cambiaste de dislike a like. Crédito descontado.");
                        return true;
                    }
                    else if (interactionType == "dislike")
                    {
                        // Cambio de like a dislike: no se afectan créditos
                        existingInteraction.interaction_type = "dislike";
                        existingInteraction.interaction_date = DateTime.Today; // Actualizar fecha
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
                        // Nuevo dislike: no afecta créditos
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
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CampusLove.Application.Services
{
    public class UserService
    {
        private readonly IUsersRepository _repository;
        private readonly IInteractionCreditsRepository _creditsRepo;
        private readonly IInteractionsRepository _interactionsRepo;
        private readonly IMatchesRepository _matchesRepo;
        private IUsersRepository userRepo;

        public UserService(
            IUsersRepository repository,
            IInteractionCreditsRepository creditsRepo,
            IInteractionsRepository interactionsRepo,
            IMatchesRepository matchesRepo)
        {
            _repository = repository;
            _creditsRepo = creditsRepo;
            _interactionsRepo = interactionsRepo;
            _matchesRepo = matchesRepo;
        }

        public UserService(IUsersRepository userRepo)
        {
            this.userRepo = userRepo;
        }

        public int CreateUser(Users user)
        {
            _repository.Create(user);
            var usuarioCreado = _repository.GetByEmail(user.email);
            if (usuarioCreado != null)
            {
                _creditsRepo.Add(new InteractionCredits
                {
                    id_user = usuarioCreado.id_user,
                    available_credits = 10,
                    last_update_date = DateTime.Today
                });
            }
            return usuarioCreado?.id_user ?? 0;
        }

        public Users? ObtenerPorId(int id) => _repository.GetById(id);

        public IEnumerable<Users> ObtenerTodos() => _repository.GetAll();

        public void Actualizar(Users user) => _repository.Update(user);

        public void Eliminar(int id) => _repository.Delete(id);

        public Users? GetByEmail(string email) => _repository.GetByEmail(email);

        public bool ValidarEmailUnico(string email) =>
            _repository.GetByEmail(email) == null;

        public bool ExisteEmail(string email) =>
            _repository.GetAll().Any(u => u.email.Equals(email, StringComparison.OrdinalIgnoreCase));

        public int ObtenerCreditosDisponibles(int id_user)
        {
            var creditos = _creditsRepo.GetByUserId(id_user);

            if (creditos == null)
                return 0;

            if (creditos.last_update_date.Date != DateTime.Today)
            {
                creditos.available_credits = 10;
                creditos.last_update_date = DateTime.Today;
                _creditsRepo.Update(creditos);
            }

            return creditos.available_credits;
        }

        public bool PuedeDarLike(int id_user)
        {
            return ObtenerCreditosDisponibles(id_user) > 0;
        }

        public void RegistrarInteraccion(int id_user_origin, int id_user_target, string tipo)
        {
            if (tipo == "like")
            {
                var creditos = _creditsRepo.GetByUserId(id_user_origin);
                if (creditos == null || ObtenerCreditosDisponibles(id_user_origin) <= 0)
                    throw new InvalidOperationException("No tienes créditos disponibles.");

                creditos.available_credits--;
                _creditsRepo.Update(creditos);
            }

            _interactionsRepo.Add(new Interactions
            {
                id_user_origin = id_user_origin,
                id_user_target = id_user_target,
                interaction_type = tipo,
                interaction_date = DateTime.Now
            });

            if (tipo == "like")
            {
                bool interaccionMutua = _interactionsRepo.HasInteractedWith(id_user_target, id_user_origin);

                if (interaccionMutua)
                {
                    _matchesRepo.Insert(new Matches
                    {
                        id_user1 = Math.Min(id_user_origin, id_user_target),
                        id_user2 = Math.Max(id_user_origin, id_user_target),
                        match_date = DateTime.Now
                    });
                }
            }
        }

        public bool YaInteractuo(int id_user_origin, int id_user_target)
        {
            return _interactionsRepo.HasInteractedWith(id_user_origin, id_user_target);
        }
    }
}

using CampusLove.Domain.Interfaces;
using CampusLove.Domain.Entities;
using System.Collections.Generic;

namespace CampusLove.Application.Services
{
    public class UsersInterestsService
    {
         // Campos privados para los repositorios inyectados
        private readonly IUsersInterestsRepository _repository;

    
        public UsersInterestsService(IUsersInterestsRepository repository)
        {
            _repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
        }

        //asocia un interes a un usuario
        public void AsociarIntereses(int id_user, IEnumerable<int> intereses)
        {
            _repository.AddUserInterests(id_user, intereses);
        }
        

        public void Guardar(int userId, int interesId)
        {
            _repository.AddUserInterest(userId, interesId);
        }

        public IEnumerable<UsersInterests> GetUserInterests(int userId)
        {
            return _repository.GetByUserId(userId);
        }
        public bool TieneInteres(int userId, int interesId)
        {
            var intereses = _repository.GetByUserId(userId);
            foreach (var interes in intereses)
            {
                if (interes.id_interest == interesId)
                {
                    return true;
                }
            }
            return false;
        }

    }
}

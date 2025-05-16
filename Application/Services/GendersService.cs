using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Application.Services
{
    public class GendersService
    {
        private readonly IGendersRepository _repository;

        public GendersService(IGendersRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Genders> GetAll()
        {
            return _repository.GetAll();
        }

        public Genders? GetById(int id)
        {
            return _repository.GetById(id);
        }
    }
}

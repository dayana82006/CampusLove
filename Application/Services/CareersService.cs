using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Application.Services
{
    public class CareersService
    {
        private readonly ICareersRepository _repository;

        public CareersService(ICareersRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Careers> GetAll()
        {
            return _repository.GetAll();
        }

        public Careers? GetById(int id)
        {
            return _repository.GetById(id);
        }
    }
}

using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Application.Services
{
    public class AddressesService
    {
        private readonly IAddressesRepository _repository;

        public AddressesService(IAddressesRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Addresses> GetAll()
        {
            return _repository.GetAll();
        }

        public Addresses? GetById(int id)
        {
            return _repository.GetById(id);
        }
    }
}

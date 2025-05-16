using CampusLove.Domain.Entities;

namespace CampusLove.Domain.Interfaces
{
    public interface IAddressesRepository
    {
        IEnumerable<Addresses> GetAll();
        Addresses GetById(int id);
    }
}

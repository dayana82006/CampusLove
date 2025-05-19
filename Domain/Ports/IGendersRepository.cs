using CampusLove.Domain.Entities;
using CampusLove.Domain.Ports;

namespace CampusLove.Domain.Interfaces
{
    public interface IGendersRepository : IGenericRepository<Genders>
    {
        IEnumerable<Genders> GetAll();
        Genders? GetById(int id);
    }
}

using CampusLove.Domain.Entities;
using CampusLove.Domain.Ports;

namespace CampusLove.Domain.Interfaces
{
    public interface ICareersRepository : IGenericRepository<Careers>
    {
        IEnumerable<Careers> GetAll();
        Careers? GetById(int id);
    }
}

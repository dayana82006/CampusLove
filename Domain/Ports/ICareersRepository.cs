using CampusLove.Domain.Entities;

namespace CampusLove.Domain.Interfaces
{
    public interface ICareersRepository
    {
        IEnumerable<Careers> GetAll();
        Careers? GetById(int id);
    }
}

using CampusLove.Domain.Entities;

namespace CampusLove.Domain.Interfaces
{
    public interface IGendersRepository
    {
        IEnumerable<Genders> GetAll();
        Genders? GetById(int id);
    }
}

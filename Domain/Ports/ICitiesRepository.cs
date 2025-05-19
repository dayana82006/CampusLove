using CampusLove.Domain.Entities;
using CampusLove.Domain.Ports;

namespace CampusLove.Domain.Interfaces
{
    public interface ICitiesRepository : IGenericRepository<Country>
    {
        void Insert(Cities city);
        Cities? GetById(int id);
        IEnumerable<Cities> GetAll();
        void Update(Cities city);
       
    }

}
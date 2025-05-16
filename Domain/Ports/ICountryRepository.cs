using CampusLove.Domain.Entities;
using CampusLove.Domain.Ports;

namespace CampusLove.Domain.Interfaces
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<Country?> GetCountryByNameAsync(string name);
    }

}
using CampusLove.Domain.Entities;
using CampusLove.Domain.Ports;

namespace CampusLove.Domain.Interfaces
{
    public interface IInterestsRepository : IGenericRepository<Interests>
    {
        
        IEnumerable<InterestsCategory> GetAllInterestsCategory();
        InterestsCategory? GetInterestsCategoryById(int id);
        IEnumerable<Interests> GetAll();
        Interests GetById(int id);
    }
}

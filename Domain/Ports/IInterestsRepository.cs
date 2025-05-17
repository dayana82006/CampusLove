using CampusLove.Domain.Entities;

namespace CampusLove.Domain.Interfaces
{
    public interface IInterestsRepository
    {
        IEnumerable<InterestsCategory> GetAllInterestsCategory();
        InterestsCategory? GetInterestsCategoryById(int id);
        IEnumerable<Interests> GetAll();
        Interests GetById(int id);
    }
}

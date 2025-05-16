using CampusLove.Domain.Entities;

namespace CampusLove.Domain.Interfaces
{
    public interface IUsersInterestsRepository
    {
        void AddUserInterest(int id_user, int id_interest);
        void AddUserInterests(int id_user, IEnumerable<int> interests);
        IEnumerable<UsersInterests> GetByUserId(int userId);
    }
}

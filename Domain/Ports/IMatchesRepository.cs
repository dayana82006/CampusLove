using CampusLove.Domain.Entities;

namespace CampusLove.Domain.Interfaces
{
    public interface IMatchesRepository
    {
        void Insert(Matches match);
        bool MatchExists(int userId1, int userId2);
    }
}

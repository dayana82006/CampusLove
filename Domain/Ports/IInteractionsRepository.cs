using CampusLove.Domain.Entities;

namespace CampusLove.Domain.Interfaces
{
    public interface IInteractionsRepository
    {
        void Add(Interactions interaction);
        IEnumerable<Interactions> GetAll();
        IEnumerable<Interactions> GetByUserId(int userId);
        IEnumerable<Interactions> GetByDate(DateTime date);
        bool HasInteractedWith(int userId, int targetUserId);

        bool Exists(int userId, int targetUserId);
        Interactions? GetInteraction(int originId, int targetId); 
        IEnumerable<Interactions> GetByUserIdAndDate(int userId, DateTime date);

    }
}

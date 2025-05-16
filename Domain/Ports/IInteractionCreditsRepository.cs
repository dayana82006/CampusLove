using CampusLove.Domain.Entities;

namespace CampusLove.Domain.Interfaces
{
    public interface IInteractionsCreditsRepository
    {
        int GetCreditsByUserId(int userId);
        void UpdateCredits(int userId, int creditChange);
        void SetInitialCredits(int userId, int initialCredits);
        InteractionCredits GetByUserId(int userId);
        void Add(InteractionCredits credits);
        void Update(InteractionCredits credits);
    }
}
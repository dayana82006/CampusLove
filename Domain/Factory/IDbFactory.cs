using CampusLove.Domain.Entities;

namespace CampusLove.Domain.Interfaces
{
    public interface IDbFactory
    {
        // ICountryRepository CreateCountryRepository();

        IUsersRepository CreateUsersRepository();
        IAddressesRepository CreateAddressesRepository();
        ICareersRepository CreateCareersRepository();
        IGendersRepository CreateGendersRepository();

        IInterestsRepository CreateInterestsRepository();
        IUsersInterestsRepository CreateUsersInterestsRepository();
                IInteractionsRepository CreateInteractionsRepository();
        IInteractionCreditsRepository CreateInteractionCreditsRepository();
        IMatchesRepository CreateMatchesRepository();
   
    }
} 
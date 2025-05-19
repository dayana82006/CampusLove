using CampusLove.Domain.Ports;

namespace CampusLove.Domain.Interfaces
{
    public interface IDbFactory
    {
        IUsersRepository CreateUsersRepository();
        IGendersRepository CreateGendersRepository();
        ICareersRepository CreateCareersRepository();
        IAddressesRepository CreateAddressesRepository();
        IInterestsRepository CreateInterestsRepository();
        IUsersInterestsRepository CreateUsersInterestsRepository();
        IInteractionsCreditsRepository CreateInteractionCreditsRepository();
        IMatchesRepository CreateMatchesRepository();
        IInteractionsRepository CreateInteractionsRepository();
        ICountryRepository CreateCountryRepository();
        IStatesRepository CreateStatesRepository();
        ICitiesRepository CreateCitiesRepository();
        ICategoryRepository CreateCategoryRepository();
    }
}

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
<<<<<<< HEAD
        IInteractionsRepository CreateInteractionsRepository();
        ICountryRepository CreateCountryRepository();
        IStatesRepository CreateStatesRepository();
        ICitiesRepository CreateCitiesRepository();
        ICategoryRepository CreateCategoryRepository();
=======
        IUserStatisticsRepository CreateUserStatisticsRepository();
        IMessagesRepository CreateMessagesRepository();
   
>>>>>>> b9a09a6ad589636af32354094a805d4d80421b32
    }
}

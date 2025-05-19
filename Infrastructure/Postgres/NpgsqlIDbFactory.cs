using CampusLove.Domain.Interfaces;   // Interfaz IDbFactory y repositorios
using CampusLove.Domain.Ports;
using CampusLove.Infrastructure.Repositories;  // Implementaciones Pgsql*
using Npgsql;

namespace CampusLove.Infrastructure.Factories
{
    public class NpgsqlDbFactory : IDbFactory
    {
        private readonly string _connectionString;

        public NpgsqlDbFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IUsersRepository CreateUsersRepository()
        {
            return new PgsqlUsersRepository(_connectionString);
        }

        public IGendersRepository CreateGendersRepository()
        {
            return new PgsqlGendersRepository(_connectionString);
        }

        public ICareersRepository CreateCareersRepository()
        {
            return new PgsqlCareersRepository(_connectionString);
        }

        public IAddressesRepository CreateAddressesRepository()
        {
            return new PgsqlAddressesRepository(_connectionString);
        }
        public IInterestsRepository CreateInterestsRepository()
        {
            return new PgsqlInterestsRepository(_connectionString);
        }
        public IUsersInterestsRepository CreateUsersInterestsRepository()
        {
            return new PgsqlUsersInterestsRepository(_connectionString);
        }
        public IInteractionsCreditsRepository CreateInteractionCreditsRepository()
        {
            return new PgsqlInteractionCreditsRepository(_connectionString);
        }
        public IMatchesRepository CreateMatchesRepository()
        {
            return new PgsqlMatchesRepository(_connectionString);
        }

        public IInteractionsRepository CreateInteractionsRepository()
        {
            return new PgsqlInteractionsRepository(_connectionString);
        }
        public ICountryRepository CreateCountryRepository()
        {
            return new PgsqlCountryRepository(_connectionString);
        }
        public IStatesRepository CreateStatesRepository()
        {
            return new PgsqlStatesRepository(_connectionString);
        }
        public ICitiesRepository CreateCitiesRepository()
        {
            return new PgsqlCitiesRepository(_connectionString);
        }

        public ICategoryRepository CreateCategoryRepository()
        {
           return new PgsqlCategoryRepository(_connectionString);
        }
    }

    
}

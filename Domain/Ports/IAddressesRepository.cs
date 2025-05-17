using CampusLove.Domain.Entities;

namespace CampusLove.Domain.Interfaces
{
    public interface IAddressesRepository
    {
        Addresses Create(Addresses newAddress);
        IEnumerable<Addresses> GetAll();
        Addresses GetById(int id);
        IEnumerable<Countries> GetAllCountries();
        IEnumerable<States> GetStatesByCountry(int id_country);
        IEnumerable<Cities> GetCitiesByState(int id_state);
        Addresses? BuscarDireccion(int id_city, string street_number, string street_name);
        Cities GetCityById(int id);
        States GetStateById(int id);
        Countries GetCountryById(int id);
    }
}

using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CampusLove.Application.Services
{
    public class AddressesService
    {
        private readonly IAddressesRepository _repository;
        private readonly string _connStr;

        public AddressesService(IAddressesRepository repository, string connStr)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _connStr = connStr ?? throw new ArgumentNullException(nameof(connStr));
        }

       public string GetFullAddress(int idAddress)
        {
            var address = _repository.GetById(idAddress);
            if (address == null)
                return "Dirección no especificada";

            var city = _repository.GetCityById(address.id_city);
            var state = city != null ? _repository.GetStateById(city.id_state) : null;
            var country = state != null ? _repository.GetCountryById(state.id_country) : null;

            var parts = new List<string>();

            if (city != null && !string.IsNullOrWhiteSpace(city.city_name))
                parts.Add(city.city_name);

            if (state != null && !string.IsNullOrWhiteSpace(state.state_name))
                parts.Add(state.state_name);

            if (country != null && !string.IsNullOrWhiteSpace(country.name_country))
                parts.Add(country.name_country);

            return string.Join(", ", parts);
        }

        public IEnumerable<Addresses> GetAll()
        {
            return _repository.GetAll();
        }

        public Addresses? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void CrearAddress(Addresses address)
        {
            if (address == null) throw new ArgumentNullException(nameof(address));
            _repository.Create(address);
        }

        public IEnumerable<Countries> GetAllCountries()
        {
            return _repository.GetAllCountries().ToList();
        }

        public IEnumerable<States> GetStatesByCountry(int id_country)
        {
            return _repository.GetStatesByCountry(id_country).ToList();
        }

        public IEnumerable<Cities> GetCitiesByState(int id_state)
        {
            return _repository.GetCitiesByState(id_state).ToList();
        }

        public int ObtenerOCrearDireccion(Addresses nuevaDireccion)
        {
            if (nuevaDireccion == null) throw new ArgumentNullException(nameof(nuevaDireccion));

            if (string.IsNullOrWhiteSpace(nuevaDireccion.street_number))
                throw new Exception("El número de calle no puede ser vacío o nulo.");

            if (string.IsNullOrWhiteSpace(nuevaDireccion.street_name))
                throw new Exception("El nombre de la calle no puede ser vacío o nulo.");

            using var connection = new NpgsqlConnection(_connStr);
            connection.Open();

            using var command = new NpgsqlCommand(
                "INSERT INTO addresses (id_city, street_number, street_name) VALUES (@id_city, @street_number, @street_name) RETURNING id_address",
                connection);

            command.Parameters.AddWithValue("@id_city", nuevaDireccion.id_city);
            command.Parameters.AddWithValue("@street_number", nuevaDireccion.street_number);
            command.Parameters.AddWithValue("@street_name", nuevaDireccion.street_name);

            var result = command.ExecuteScalar();
            if (result != null && int.TryParse(result.ToString(), out int id))
            {
                return id;
            }

            throw new Exception("No se pudo insertar la dirección.");
        }
    }
}

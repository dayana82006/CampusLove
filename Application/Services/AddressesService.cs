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
         // Campos privados para los repositorios inyectados
        private readonly IAddressesRepository _repository;
        private readonly string _connStr;
        // Constructor
        public AddressesService(IAddressesRepository repository, string connStr)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _connStr = connStr ?? throw new ArgumentNullException(nameof(connStr));
        }

        

        //metodo para obtener la dirección completa
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

            if (country != null && !string.IsNullOrWhiteSpace(country.country_name))
                parts.Add(country.country_name);

            return string.Join(", ", parts);
        }

        // Método para mostrar todas las direcciones
        public IEnumerable<Addresses> GetAll()
        {
            return _repository.GetAll();
        }

        // Método para obtener una dirección por ID
        public Addresses? GetById(int id)
        {
            return _repository.GetById(id);
        }

        // Método para crear una nueva dirección
        public void CrearAddress(Addresses address)
        {
            if (address == null) throw new ArgumentNullException(nameof(address));
            _repository.Create(address);
        }

        // Método para obtener todas los países
        public IEnumerable<Country> GetAllCountries()
        {
            return _repository.GetAllCountries().ToList();
        }
        // Método para obtener un estado por pais
        public IEnumerable<States> GetStatesByCountry(int id_country)
        {
            return _repository.GetStatesByCountry(id_country).ToList();
        }

        // Método para obtener una ciudad por estado
        public IEnumerable<Cities> GetCitiesByState(int id_state)
        {
            return _repository.GetCitiesByState(id_state).ToList();
        }

        // Método para obtener o crear una dirección
        public int ObtenerOCrearDireccion(Addresses nuevaDireccion)
        {
            if (nuevaDireccion == null) throw new ArgumentNullException(nameof(nuevaDireccion));

            if (string.IsNullOrWhiteSpace(nuevaDireccion.street_number))
                throw new Exception("El número de calle no puede ser vacío o nulo.");

            if (string.IsNullOrWhiteSpace(nuevaDireccion.street_name))
                throw new Exception("El nombre de la calle no puede ser vacío o nulo.");

            using var connection = new NpgsqlConnection(_connStr);
            connection.Open();

            // 1. Intentar buscar dirección existente
            using (var selectCmd = new NpgsqlCommand(
                "SELECT id_address FROM addresses WHERE id_city = @id_city AND street_number = @street_number AND street_name = @street_name",
                connection))
            {
                selectCmd.Parameters.AddWithValue("@id_city", nuevaDireccion.id_city);
                selectCmd.Parameters.AddWithValue("@street_number", nuevaDireccion.street_number);
                selectCmd.Parameters.AddWithValue("@street_name", nuevaDireccion.street_name);

                var existingId = selectCmd.ExecuteScalar();
                if (existingId != null && int.TryParse(existingId.ToString(), out int foundId))
                {
                    return foundId; // Retorna dirección existente
                }
            }

            // 2. Si no existe, insertar nueva dirección
            using var insertCmd = new NpgsqlCommand(
                "INSERT INTO addresses (id_city, street_number, street_name) VALUES (@id_city, @street_number, @street_name) RETURNING id_address",
                connection);

            insertCmd.Parameters.AddWithValue("@id_city", nuevaDireccion.id_city);
            insertCmd.Parameters.AddWithValue("@street_number", nuevaDireccion.street_number);
            insertCmd.Parameters.AddWithValue("@street_name", nuevaDireccion.street_name);

            var insertedId = insertCmd.ExecuteScalar();
            if (insertedId != null && int.TryParse(insertedId.ToString(), out int newId))
            {
                return newId;
            }

            throw new Exception("No se pudo insertar la dirección.");
        }

        // Método para eliminar una dirección
        public void Delete(int id)
        {
            if (id <= 0) throw new ArgumentException("El ID debe ser mayor a cero.");

            _repository.Delete(id);
        }

    }
}

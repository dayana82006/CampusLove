using System.Collections.Generic;
using Npgsql;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Infrastructure.Repositories 
{
    public class PgsqlAddressesRepository : IAddressesRepository
    {
        private readonly string _connectionString;

        public PgsqlAddressesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Addresses> GetAll()
        {
            var list = new List<Addresses>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_address, id_city, street_number, street_name FROM addresses", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Addresses
                {
                    id_address = reader.GetInt32(0),
                    id_city = reader.GetInt32(1),
                    street_number = reader.IsDBNull(2) ? null : reader.GetString(2),
                    street_name = reader.IsDBNull(3) ? null : reader.GetString(3)
                });
            }

            return list;
        }

        public Addresses GetById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_address, id_city, street_number, street_name FROM addresses WHERE id_address = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Addresses
                {
                    id_address = reader.GetInt32(0),
                    id_city = reader.GetInt32(1),
                    street_number = reader.IsDBNull(2) ? null : reader.GetString(2),
                    street_name = reader.IsDBNull(3) ? null : reader.GetString(3)
                };
            }

            return null;
        }

        public Cities GetCityById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_city, city_name, id_state FROM cities WHERE id_city = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Cities
                {
                    id_city = reader.GetInt32(0),
                    city_name = reader.GetString(1),
                    id_state = reader.GetInt32(2)
                };
            }

            return null;
        }

        public States GetStateById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_state, state_name, id_country FROM states WHERE id_state = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new States
                {
                    id_state = reader.GetInt32(0),
                    state_name = reader.GetString(1),
                    id_country = reader.GetInt32(2)  
                };
            }

            return null;
        }

        public Country GetCountryById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_country, country_name FROM countries WHERE id_country = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Country
                {
                    id_country = reader.GetInt32(0),
                    country_name = reader.GetString(1)
                };
            }

            return null;
        }

        public Addresses Create(Addresses address)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand(
                "INSERT INTO addresses (id_city, street_number, street_name) VALUES (@idCity, @streetNumber, @streetName) RETURNING id_address", connection);

            command.Parameters.AddWithValue("@idCity", address.id_city);
            command.Parameters.AddWithValue("@streetNumber", address.street_number ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@streetName", address.street_name ?? (object)DBNull.Value);

            var newId = (int)command.ExecuteScalar();
            address.id_address = newId;

            return address;
        }

        public IEnumerable<Country> GetAllCountries()
        {
            var list = new List<Country>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_country, country_name FROM countries", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Country
                {
                    id_country = reader.GetInt32(0),
                    country_name = reader.GetString(1)
                });
            }

            return list;
        }

        public IEnumerable<States> GetStatesByCountry(int id_country)
        {
            var list = new List<States>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_state, state_name FROM states WHERE id_country = @id_country", connection);
            command.Parameters.AddWithValue("@id_country", id_country);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new States
                {
                    id_state = reader.GetInt32(0),
                    state_name = reader.GetString(1)
                });
            }

            return list;
        }

        public IEnumerable<Cities> GetCitiesByState(int id_state)
        {
            var list = new List<Cities>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_city, city_name FROM cities WHERE id_state = @id_state", connection);
            command.Parameters.AddWithValue("@id_state", id_state);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Cities
                {
                    id_city = reader.GetInt32(0),
                    city_name = reader.GetString(1)
                });
            }

            return list;
        }

        public Addresses BuscarDireccion(int id_city, string street_number, string street_name)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand(
                "SELECT id_address, id_city, street_number, street_name FROM addresses WHERE id_city = @id_city AND street_number = @street_number AND street_name = @street_name", connection);

            command.Parameters.AddWithValue("@id_city", id_city);
            command.Parameters.AddWithValue("@street_number", street_number);
            command.Parameters.AddWithValue("@street_name", street_name);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Addresses
                {
                    id_address = reader.GetInt32(0),
                    id_city = reader.GetInt32(1),
                    street_number = reader.IsDBNull(2) ? null : reader.GetString(2),
                    street_name = reader.IsDBNull(3) ? null : reader.GetString(3)
                };
            }

            return null;
        }

        IEnumerable<Country> IAddressesRepository.GetAllCountries()
        {
            throw new NotImplementedException();
        }
    }
}

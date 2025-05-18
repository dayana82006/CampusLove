using System;
using System.Collections.Generic;
using Npgsql;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Infrastructure.Repositories
{
    public class PgsqlCountryRepository : ICountryRepository
    {
        private readonly string _connectionString;

        public PgsqlCountryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Country> GetAll()
        {
            var countries = new List<Country>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_country, country_name FROM countries", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                countries.Add(new Country
                {
                    id_country = reader.GetInt32(0),
                    country_name = reader.GetString(1)
                });
            }

            return countries;
        }

        public Country GetById(int id)
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

        public Task<Country?> GetCountryByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public void Create(Country entity)
{
    using var connection = new NpgsqlConnection(_connectionString);
    connection.Open();

    var query = "INSERT INTO countries (country_name) VALUES (@country_name)";
    using var command = new NpgsqlCommand(query, connection);
    command.Parameters.AddWithValue("@country_name", entity.country_name);

    command.ExecuteNonQuery();
}

public void Update(Country entity)
{
    using var connection = new NpgsqlConnection(_connectionString);
    connection.Open();

    var query = "UPDATE countries SET country_name = @country_name WHERE id_country = @id_country";
    using var command = new NpgsqlCommand(query, connection);
    command.Parameters.AddWithValue("@country_name", entity.country_name);
    command.Parameters.AddWithValue("@id_country", entity.id_country);

    command.ExecuteNonQuery();
}

public void Delete(int id)
{
    using var connection = new NpgsqlConnection(_connectionString);
    connection.Open();

    var query = "DELETE FROM countries WHERE id_country = @id_country";
    using var command = new NpgsqlCommand(query, connection);
    command.Parameters.AddWithValue("@id_country", id);

    command.ExecuteNonQuery();
}

    }
}

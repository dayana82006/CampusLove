using System.Collections.Generic;
using Npgsql;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Domain.Interfaces 
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

            var command = new NpgsqlCommand("SELECT id_address, id_city FROM addresses", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Addresses
                {
                    id_address = reader.GetInt32(0),
                    id_city = reader.GetInt32(1)
                });
            }

            return list;
        }

        public Addresses GetById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_address, id_city FROM addresses WHERE id_address = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Addresses
                {
                    id_address = reader.GetInt32(0),
                    id_city = reader.GetInt32(1)
                };
            }

            return null;
        }
    }
}

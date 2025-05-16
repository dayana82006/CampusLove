using System.Collections.Generic;
using Npgsql;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Infrastructure.Repositories
{
    public class PgsqlInterestsRepository : IInterestsRepository
    {
        private readonly string _connectionString;

        public PgsqlInterestsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Interests> GetAll()
        {
            var list = new List<Interests>();
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var command = new NpgsqlCommand("SELECT id_interest, interest_name, id_category FROM Interests", connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Interests
                {
                    id_interest = reader.GetInt32(0),
                    interest_name = reader.GetString(1),
                    id_category = reader.GetInt32(2)
                });
            }
            return list;
        }

        public IEnumerable<Interests> GetInterestsByCategory(int categoryId)
        {
            var list = new List<Interests>();
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var command = new NpgsqlCommand("SELECT id_interest, interest_name FROM Interests WHERE id_category = @categoryId", connection);
            command.Parameters.AddWithValue("@categoryId", categoryId); 
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Interests
                {
                    id_interest = reader.GetInt32(0),
                    interest_name = reader.GetString(1)
                });
            }
            return list;
        }

        public IEnumerable<InterestsCategory> GetAllInterestsCategory()
        {
            var list = new List<InterestsCategory>();
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            var command = new NpgsqlCommand("SELECT id_category, interest_category FROM InterestsCategory", connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new InterestsCategory
                {
                    id_category = reader.GetInt32(0),
                    interest_category = reader.GetString(1)
                });
            }
            return list;
        }

        public InterestsCategory? GetInterestsCategoryById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_category, interest_category FROM InterestsCategory WHERE id_category = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new InterestsCategory
                {
                    id_category = reader.GetInt32(0),
                    interest_category = reader.GetString(1)
                };
            }

            return null;
        }

        public Interests? GetById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var command = new NpgsqlCommand("SELECT id_interest, interest_name FROM Interests WHERE id_interest = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Interests
                {
                    id_interest = reader.GetInt32(0),
                    interest_name = reader.GetString(1)
                };
            }

            return null;
        }
    }
}

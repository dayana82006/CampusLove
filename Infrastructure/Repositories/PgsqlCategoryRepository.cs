using System;
using System.Collections.Generic;
using Npgsql;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;
using CampusLove.Domain.Ports;

namespace CampusLove.Infrastructure.Repositories;

public class PgsqlCategoryRepository : ICategoryRepository
{
    private readonly string _connectionString;

    public PgsqlCategoryRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IEnumerable<Category> GetAll()
    {
        var categories = new List<Category>();

        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var command = new NpgsqlCommand("SELECT id_category, interest_category FROM InterestsCategory", connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            categories.Add(new Category
            {
                id_category = reader.GetInt32(0),
                interest_category = reader.GetString(1)
            });
        }

        return categories;
    }
    public void Create(Category entity)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        var command = new NpgsqlCommand("INSERT INTO InterestsCategory (interest_category) VALUES (@interest_category)", connection);
        command.Parameters.AddWithValue("@interest_category", entity.interest_category);
        command.ExecuteNonQuery();

    }

    public void Delete(int id)
    {
       using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var command = new NpgsqlCommand("DELETE FROM InterestsCategory WHERE id_category = @id", connection);
        command.Parameters.AddWithValue("@id", id);

        command.ExecuteNonQuery();
    }


    public Category? GetById(int id)
    {
       using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var command = new NpgsqlCommand("SELECT id_category, interest_category FROM InterestsCategory WHERE id_category = @id", connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Category
            {
                id_category = reader.GetInt32(0),
                interest_category = reader.GetString(1)
            };
        }

        return null;
    }

    public Task<Category?> GetCategoryByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public void Update(Category entity)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var command = new NpgsqlCommand("UPDATE InterestsCategory SET interest_category = @interest_category WHERE id_category = @id", connection);
        command.Parameters.AddWithValue("@interest_category", entity.interest_category);
        command.Parameters.AddWithValue("@id", entity.id_category);

        command.ExecuteNonQuery();
    }
}
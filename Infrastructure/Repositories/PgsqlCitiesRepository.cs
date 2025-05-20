
using Npgsql;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;
using CampusLove.Domain.Ports;
namespace CampusLove.Infrastructure.Repositories;

public class PgsqlCitiesRepository : ICitiesRepository
{

    private readonly string _connStr;
    public PgsqlCitiesRepository(string connStr)
    {
        _connStr = connStr;
    }
    public void Insert(Cities city)
    {
        using var conn = new NpgsqlConnection(_connStr);
        conn.Open();
       using var cmd = new NpgsqlCommand("INSERT INTO cities ( city_name,id_state) VALUES ( @city_name,@id_state)", conn);

        cmd.Parameters.AddWithValue("city_name", city.city_name);
        cmd.Parameters.AddWithValue("id_state", city.id_state);
        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var conn = new NpgsqlConnection(_connStr);
        conn.Open();
        using var cmd = new NpgsqlCommand("DELETE FROM cities WHERE id_city = @id_city", conn);
        cmd.Parameters.AddWithValue("id_city", id);
        cmd.ExecuteNonQuery();
    }

    public IEnumerable<Cities> GetAll()
    {
        using var conn = new NpgsqlConnection(_connStr);
        conn.Open();
        using var cmd = new NpgsqlCommand("SELECT * FROM cities", conn);
        using var reader = cmd.ExecuteReader();
        var cities = new List<Cities>();
        while (reader.Read())
        {
            var city = new Cities
            {
                id_city = reader.GetInt32(0),
                city_name = reader.GetString(1),
                id_state = reader.GetInt32(2)
            };
            cities.Add(city);
        }
        return cities;
    }

    public Cities? GetById(int id)
    {
        using var conn = new NpgsqlConnection(_connStr);
        conn.Open();
        using var cmd = new NpgsqlCommand("SELECT * FROM cities WHERE id_city = @id_city", conn);
        cmd.Parameters.AddWithValue("id_city", id);
        using var reader = cmd.ExecuteReader();
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

    public void Update(Cities city)
    {
        using var conn = new NpgsqlConnection(_connStr);
        conn.Open();
        using var cmd = new NpgsqlCommand("UPDATE cities SET city_name = @city_name, id_state = @id_state   WHERE id_city = @id_city", conn);
        cmd.Parameters.AddWithValue("id_city", city.id_city);
        cmd.Parameters.AddWithValue("city_name", city.city_name);
        cmd.Parameters.AddWithValue("id_state", city.id_state);
        cmd.ExecuteNonQuery();
    }


    IEnumerable<Country> IGenericRepository<Country>.GetAll()
    {
        using var conn = new NpgsqlConnection(_connStr);
        conn.Open();
        using var cmd = new NpgsqlCommand("SELECT * FROM countries", conn);
        using var reader = cmd.ExecuteReader();
        var countries = new List<Country>();
        while (reader.Read())
        {
            var country = new Country
            {
                id_country = reader.GetInt32(0),
                country_name = reader.GetString(1)
            };
            countries.Add(country);
        }
        return countries;
    }

    Country? IGenericRepository<Country>.GetById(int id)
    {
        using var conn = new NpgsqlConnection(_connStr);
        conn.Open();
        using var cmd = new NpgsqlCommand("SELECT * FROM countries WHERE id_country = @id_country", conn);
        cmd.Parameters.AddWithValue("id_country", id);
        using var reader = cmd.ExecuteReader();
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

    public void Create(Country entity)
    {
        using var conn = new NpgsqlConnection(_connStr);
        conn.Open();
        using var cmd = new NpgsqlCommand("INSERT INTO countries (country_name) VALUES (@country_name)", conn);
        cmd.Parameters.AddWithValue("country_name", entity.country_name);
        cmd.ExecuteNonQuery();
    }

    public void Update(Country entity)
    {
        using var conn = new NpgsqlConnection(_connStr);
        conn.Open();
        using var cmd = new NpgsqlCommand("UPDATE countries SET country_name = @country_name WHERE id_country = @id_country", conn);
        cmd.Parameters.AddWithValue("id_country", entity.id_country);
        cmd.Parameters.AddWithValue("country_name", entity.country_name);
        cmd.ExecuteNonQuery();
    }

}

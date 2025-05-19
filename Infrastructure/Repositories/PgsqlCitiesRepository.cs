
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
        throw new NotImplementedException();
    }

    Country? IGenericRepository<Country>.GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Create(Country entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Country entity)
    {
        throw new NotImplementedException();
    }

}

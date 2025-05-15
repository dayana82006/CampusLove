using CampusLove.Domain.Entities;
using CampusLove.Domain.Ports;
using CampusLove.Infrastructure.Mysql;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace CampusLove.Infrastructure.Repositories;

public class ImpPaisRepository : IGenericRepository<Pais>, IPaisRepository
{
    private readonly ConexionSingleton _conexion;

    public ImpPaisRepository(string conexion)
    {
        _conexion = ConexionSingleton.Instancia(conexion);
    }

    public List<Pais> ObtenerTodos()
    {
        var pais = new List<Pais>();
        var connection = _conexion.ObtenerConexion();
        try
        {
            string query = "SELECT id, nombre FROM pais";
            using var cmd = new MySqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                pais.Add(new Pais
                {
                    id = reader.GetInt32("id"),
                    nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader.GetString("nombre"),

                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al obtener todos los paises: {ex.Message}");
        }

        return pais;
    }
    public void Crear(Pais pais)
    {
        try
        {
            var connection = _conexion.ObtenerConexion();
            string query = "INSERT INTO pais (id, nombre) VALUES (@id, @nombre)";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", pais.id);
            cmd.Parameters.AddWithValue("@nombre", pais.nombre);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al crear pais: {ex.Message}");
        }
    }
    public void Actualizar(Pais pais)
    {
        try
        {

            var connection = _conexion.ObtenerConexion();
            string query = "UPDATE pais SET nombre = @nombre WHERE id = @id";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", pais.id);
            cmd.Parameters.AddWithValue("@nombre", pais.nombre);
            cmd.ExecuteNonQuery();
        }

        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al actualizar pais: {ex.Message}");
        }
    }


    public void Eliminar(int id)
    {
        try
        {   var connection = _conexion.ObtenerConexion();
            string query = "DELETE FROM pais WHERE id = @id";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al eliminar pais: {ex.Message}");
        }
    }
    IEnumerable<Pais> IGenericRepository<Pais>.ObtenerTodos()
    {
        return ObtenerTodos();

    }
    public Pais? ObtenerPorId(int id)
    {
        try
        {
            var connection = _conexion.ObtenerConexion();
            string query = "SELECT id, nombre FROM pais WHERE id = @id";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Pais
                {
                    
                    id = reader.GetInt32("id"),
                    nombre = reader.GetString("nombre"),
                };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al obtener pais por ID: {ex.Message}");
        }

      
        throw new Exception("Cliente no encontrado");
    }

}

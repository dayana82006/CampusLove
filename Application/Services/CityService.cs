using System;
using System.Collections.Generic;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Application.Services;
public class CityService
{
    private readonly ICitiesRepository _repo;
    private readonly IStatesRepository _statesRepo;

public CityService(ICitiesRepository repo, IStatesRepository statesRepo)

    {
        _repo = repo;
        _statesRepo = statesRepo;
    }

    public bool CreateCity(Cities city)
    {
        if (string.IsNullOrWhiteSpace(city.city_name))
            throw new ArgumentException("El nombre de la ciudad no puede estar vacío.");

        var state = _statesRepo.GetById(city.id_state);
        if (state == null)
        {
            Console.WriteLine("❌ El estado con ese ID no existe.");
            return false;
        }

        _repo.Insert(city);
        return true;
    }

    public Cities? GetById(int id) => _repo.GetById(id);

    public List<Cities> GetAll() => _repo.GetAll().ToList();

    public bool UpdateCity(int id, string name)
    {
        var city = _repo.GetById(id);
        if (city == null)
        {
            Console.WriteLine("❌ Ciudad no encontrada.");
            return false;
        }
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("❌ El nuevo nombre no puede estar vacío.");
            return false;
        }
        city.city_name = name.Trim();
        _repo.Update(city);
        return true;
    }

    public bool DeleteCity(int id)
    {
        var existing = _repo.GetById(id);
        if (existing == null)
            return false;

        _repo.Delete(id);
        return true;
    }

    public void ShowAll()
    {
        var cities = GetAll();
        if (cities.Count == 0)
        {
            Console.WriteLine("❌ No hay ciudades disponibles.");
            return;
        }

        Console.WriteLine("📋 Lista de ciudades:");
        foreach (var city in cities)
        {
            Console.WriteLine($"ID: {city.id_city}, Nombre: {city.city_name}, EstadoID: {city.id_state}");
        }
    }
}

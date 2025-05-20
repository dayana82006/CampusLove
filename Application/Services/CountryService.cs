using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;
using CampusLove.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CampusLove.Application.Services
{
    public class CountryService
    {
         // Campos privados para los repositorios inyectados
        private readonly ICountryRepository _repo;

        public CountryService(ICountryRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        //Metodo que muestra todos los paises
        public void ShowAll()
        {
            var countries = _repo.GetAll();

            if (countries == null || !countries.Any())
            {
                Console.WriteLine("📭 No countries registered.");
                return;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n╔══════════════════════════════════════╗");
            Console.WriteLine("║          🌍 LISTA DE PAÍSES           ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();

            foreach (var country in countries)
            {
                Console.WriteLine($"ID: {country.id_country}, Name: {country.country_name}");
            }
            Console.WriteLine(new string('-', 40));
        }

        //Metodo para crear un pais
        public void CreateCountry(Country country)
        {
            _repo.Create(country);
        }

        //Metodo para actualizar un pais
        public bool UpdateCountry(int id, string name)
        {
            var country = _repo.GetById(id);

            if (country == null)
            {
                Console.WriteLine("❌ Country not found.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("❌ The new name cannot be empty.");
                return false;
            }

            country.country_name = name.Trim();
            _repo.Update(country);

            return true;
        }

        //Metodo para eliminar un pais
        public void DeleteCountry(int id)
        {
            var country = _repo.GetById(id);

            if (country == null)
            {
                Console.WriteLine("❌ Country not found.");
                return;
            }

            _repo.Delete(id);
            Console.WriteLine($"✅ Country with ID {id} successfully deleted.");
        }

        //Metodo para obtener todos los paises
        public IEnumerable<Country> GetAll()
        {
            return _repo.GetAll();
        }

        //Metodo para obtener un pais por id
        public Country GetById(int id)
        {
            return _repo.GetById(id);
        }

        
    }
}

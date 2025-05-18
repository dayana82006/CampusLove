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
        private readonly ICountryRepository _repo;

        public CountryService(ICountryRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public void ShowAll()
        {
            var countries = _repo.GetAll();

            if (countries == null || !countries.Any())
            {
                Console.WriteLine("📭 No countries registered.");
                return;
            }

            Console.WriteLine("\n--- List of Countries ---");
            foreach (var country in countries)
            {
                Console.WriteLine($"ID: {country.id_country}, Name: {country.country_name}");
            }
            Console.WriteLine(new string('-', 40));
        }

        public void CreateCountry(Country country)
        {
            _repo.Create(country);
        }

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

            country.country_name= name.Trim();
            _repo.Update(country);

            return true;
        }

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

        public IEnumerable<Country> GetAll()
        {
            return _repo.GetAll();
        }

        public Country GetById(int id)
        {
            return _repo.GetById(id);
        }
    }
}

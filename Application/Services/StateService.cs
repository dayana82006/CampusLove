using System;
using System.Collections.Generic;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Application.Services
{
    public class StateService
    {
        // Campos privados para los repositorios inyectados
        private readonly IStatesRepository _repository;

        public StateService(IStatesRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


        //Crea un estado
        public void CreateState(States state)
        {
            if (string.IsNullOrWhiteSpace(state.state_name))
                throw new ArgumentException("El nombre del estado no puede estar vacío.");

            _repository.Insert(state);
        }
        //obtiene un estado por id
        public States? GetById(int id)
        {
            return _repository.GetById(id);
        }


        //obtiene todos los estados
        public List<States> GetAll()
        {
            return _repository.GetAll().ToList();
        }
        //actualiza los estados
        public bool UpdateState(int id, string name)
        {
            var states = _repository.GetById(id);

            if (states == null)
            {
                Console.WriteLine("❌ Country not found.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("❌ The new name cannot be empty.");
                return false;
            }

            states.state_name = name.Trim();
            _repository.Update(states);

            return true;
        }
        //elimina los estados
        public bool DeleteState(int id)
        {
            var existing = _repository.GetById(id);
            if (existing == null)
                return false;

            _repository.Delete(id);
            return true;
        }

        //muestra todos los estados 
        public void ShowAll()
        {
            var estados = GetAll();
            Console.WriteLine("📋 Lista de Estados:");
            foreach (var s in estados)
            {
                Console.WriteLine($"ID: {s.id_state} | Estado: {s.state_name} | País ID: {s.id_country}");
            }
        }
    }
}

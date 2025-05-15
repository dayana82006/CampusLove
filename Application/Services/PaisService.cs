using CampusLove.Domain.Entities;
using CampusLove.Domain.Ports;
using System;
using System.Collections.Generic;

namespace CampusLove.Application.Services
{
    public class PaisService
    {
        private readonly IPaisRepository _repo;

        public PaisService(IPaisRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public void MostrarTodos()
        {
            var paises = _repo.ObtenerTodos();

            if (paises == null || !paises.Any())
            {
                Console.WriteLine("📭 No hay países registrados.");
                return;
            }

            Console.WriteLine("\n--- Lista de países ---");
            foreach (var p in paises)
            {
                Console.WriteLine($"ID: {p.id}, Nombre: {p.nombre}");
            }
            Console.WriteLine(new string('-', 40));
        }
        public void CrearPais(Pais pais)
        {
            _repo.Crear(pais);
        }

        public bool ActualizarPais(int id, string nombre)
        {
            var pais = _repo.ObtenerPorId(id);

            if (pais == null)
            {
                Console.WriteLine("❌ País no encontrado.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("❌ El nuevo nombre no puede estar vacío.");
                return false;
            }

            pais.nombre = nombre.Trim();
            _repo.Actualizar(pais);

            return true;
        }

        public void EliminarPais(int id)
        {
            var pais = _repo.ObtenerPorId(id);

            if (pais == null)
            {
                Console.WriteLine("❌ País no encontrado.");
                return;
            }

            _repo.Eliminar(id);
            Console.WriteLine($"✅ País con ID {id} eliminado con éxito.");
        }

        public IEnumerable<Pais> ObtenerTodos()
        {
            return _repo.ObtenerTodos();
        }

        public Pais ObtenerPorId(int id)
        {
            return _repo.ObtenerPorId(id);
        }
    }
}

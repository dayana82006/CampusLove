using System;
using System.Linq;
using CampusLove.Domain.Entities;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.Admin.Paises

{
    public class CrearPais
    {
        private readonly PaisService _servicio;

        public CrearPais(PaisService servicio)
        {
            _servicio = servicio;
        }

        public void Ejecutar()
        {
            var pais = new Pais();
            Console.Write("Nombre: ");
            pais.nombre = Console.ReadLine();

            _servicio.CrearPais(pais);
            Console.WriteLine("✅ País creado con éxito.");
        }
    }
}

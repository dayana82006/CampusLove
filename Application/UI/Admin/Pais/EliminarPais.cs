using System;
using CampusLove.Domain.Ports;
using CampusLove.Domain.Entities;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.Admin.Paises
{
    public class EliminarPais
    {
        private readonly PaisService _servicio;

        public EliminarPais(PaisService servicio)
        {
            _servicio = servicio;
        }

        public void Ejecutar()
        {
            Console.Write("Ingrese el ID del país a eliminar: ");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;

            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("❌ ID inválido. Debe ser un número entero.");
                return;
            }

            _servicio.EliminarPais(id);
        }
    }
}

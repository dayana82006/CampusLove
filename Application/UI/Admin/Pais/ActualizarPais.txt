using System;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.Admin.Paises
{
    public class ActualizarPais
    {
        private readonly PaisService _servicio;

        public ActualizarPais(PaisService servicio)
        {
            _servicio = servicio;
        }

       public void Ejecutar()
{
    Console.Write("ID del país a actualizar: ");
    
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("❌ ID no válido. Debe ser un número entero.");
        return;
    }

    Console.Write("Nuevo nombre: ");
    string nuevoNombre = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(nuevoNombre))
    {
        Console.WriteLine("❌ El nombre no puede estar vacío.");
        return;
    }

    bool actualizado = _servicio.ActualizarPais(id, nuevoNombre);

    if (actualizado)
    {
        Console.WriteLine("✅ País actualizado con éxito.");
    }
    else 
    {
        Console.WriteLine("❌ No se encontró un país con ese ID.");
    }
}

    }
}

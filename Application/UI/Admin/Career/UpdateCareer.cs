using System;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.Admin.Career;

public class UpdateCareer
{
private readonly CareersService _servicio;

    public UpdateCareer(CareersService servicio)
    {
        _servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));
    }

    public void Ejecutar()
    {
        Console.Write("ID de la carrera a actualizar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("❌ ID inválido.");
            return;
        }

        var career = _servicio.GetById(id);
        if (career == null)
        {
            Console.WriteLine("❌ Carrera no encontrada.");
            return;
        }

        Console.Write("Nuevo nombre de la carrera: ");
        string? newName = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(newName))
        {
            Console.WriteLine("❌ El nombre no puede estar vacío.");
            return;
        }

        career.career_name = newName;
        _servicio.Update(career);
        Console.WriteLine("✅ Carrera actualizada con éxito.");
    }
}

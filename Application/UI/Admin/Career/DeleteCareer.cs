using CampusLove.Application.Services;

namespace CampusLove.Application.UI.Admin.Career;

public class DeleteCareer
{
    private readonly CareersService _servicio;
    public DeleteCareer(CareersService servicio)
    {
        _servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));
    }
    public void Ejecutar()
    {
        Console.Write("ID de la carrera a eliminar: ");
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

        Console.WriteLine("✅ Carrera eliminada con éxito.");
    }
}

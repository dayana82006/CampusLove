using CampusLove.Application.Services;

namespace CampusLove.Application.UI.Admin.Interest;

public class DeleteInterest
{
    private readonly InterestsService _service;
    public DeleteInterest(InterestsService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    public void Ejecutar()
    {
        Console.Write("ID del interés a eliminar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("❌ ID inválido. Debe ser un número entero.");
            return;
        }

        _service.Delete(id);
        Console.WriteLine($"✅ Interés con ID '{id}' eliminado con éxito.");
    }
}

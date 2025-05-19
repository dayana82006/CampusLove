using CampusLove.Application.Services;

namespace CampusLove.Application.UI.Admin.Gender;

public class DeleteGender
{
    private readonly GendersService _service;
    public DeleteGender(GendersService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    public void Ejecutar()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔════════════════════════════════════╗");
        Console.WriteLine("║         👤 ELIMINAR GÉNERO         ║");
        Console.WriteLine("╚════════════════════════════════════╝");
        Console.ResetColor();

        Console.Write("\n📌 ID del género a eliminar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ ID inválido. Debe ser un número entero.");
            Console.ResetColor();
            return;
        }

        try
        {
            _service.Delete(id);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Género eliminado con éxito.");
            Console.ResetColor();
        }
        catch (ArgumentException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Error: {ex.Message}");
            Console.ResetColor();
            return;
        }
    }
}

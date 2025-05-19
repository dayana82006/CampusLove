using CampusLove.Application.Services;

namespace CampusLove.Application.UI.Admin.Gender;

public class UpdateGender
{
    private readonly GendersService _service;
    public UpdateGender(GendersService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    public void Ejecutar()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔════════════════════════════════════╗");
        Console.WriteLine("║         👤 ACTUALIZAR GÉNERO       ║");
        Console.WriteLine("╚════════════════════════════════════╝");
        Console.ResetColor();

        Console.Write("\n📌 ID del género a actualizar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ ID inválido. Debe ser un número entero.");
            Console.ResetColor();
            return;
        }
        var gender = _service.GetById(id);
        if (gender == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ Género no encontrado.");
            Console.ResetColor();
            return;
        }
        Console.WriteLine($"\n📌 Nuevo nombre del genero: ");
        var newName = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(newName))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ El nombre no puede estar vacío.");
            Console.ResetColor();
            return;
        }
        gender.genre_name = newName;
        _service.Update(gender);
        Console.WriteLine("\n✅ Género actualizado con éxito.");
    
    }
}

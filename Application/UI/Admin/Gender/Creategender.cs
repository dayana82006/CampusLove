using CampusLove.Application.Services;
using CampusLove.Domain.Entities;

namespace CampusLove.Application.UI.Admin.Gender;

public class Creategender
{
    private readonly GendersService _service;
    public Creategender(GendersService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
    public void Ejecutar()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔════════════════════════════════════╗");
        Console.WriteLine("║         👤 CREAR GÉNERO           ║");
        Console.WriteLine("╚════════════════════════════════════╝");
        Console.ResetColor();
        var gender = new Genders();
        Console.Write("\n📌 Nombre del género: ");
        gender.genre_name = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(gender.genre_name))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ El nombre no puede estar vacío. Intenta nuevamente.");
            Console.ResetColor();
            return;
        }
        try
        {
            _service.CreateGender(gender);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Genero creado con éxito");
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

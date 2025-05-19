using CampusLove.Application.Services;
using CampusLove.Domain.Entities;

namespace CampusLove.Application.UI.Admin.Interest;

public class CreateInterest
{
    private readonly InterestsService _service;

    public CreateInterest(InterestsService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    public void Ejecutar()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("📚 CATEGORÍAS DISPONIBLES:");
        Console.ResetColor();

        var categories = _service.GetAllInterestsCategory();
        foreach (var category in categories)
        {
            Console.WriteLine($"[{category.id_category}] - {category.interest_category}");
        }

        Console.Write("\n📝 Nombre del interés: ");
        var interestName = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(interestName))
        {
            Console.WriteLine("❌ El nombre no puede estar vacío.");
            return;
        }

        Console.Write("📂 Ingrese el ID de la categoría: ");
        if (!int.TryParse(Console.ReadLine(), out int categoryId))
        {
            Console.WriteLine("❌ El ID de categoría no es válido.");
            return;
        }

        var interest = new Interests
        {
            interest_name = interestName,
            id_category = categoryId
        };

        try
        {
            _service.Create(interest);
            Console.WriteLine($"✅ Interés '{interest.interest_name}' creado con éxito.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"❌ Error: {ex.Message}");
        }
    }
}

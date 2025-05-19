using CampusLove.Application.Services;
using CampusLove.Domain.Entities;

namespace CampusLove.Application.UI.Admin.Interest.CategoryInterest;

public class CreateCategory
{
    private readonly CategoryService _servicio;
    public CreateCategory(CategoryService servicio)
    {
        _servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));
    }
    public void Ejecutar()
    {
        var category = new Category();

        Console.Write("Nombre de la categoría: ");
        category.interest_category = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(category.interest_category))
        {
            Console.WriteLine("❌ El nombre no puede estar vacío.");
            return;
        }

        _servicio.CreateCategory(category);
        Console.WriteLine($"✅ Categoría '{category.interest_category}' creada con éxito.");
    }   
}

using CampusLove.Application.Services;

namespace CampusLove.Application.UI.Admin.Interest.CategoryInterest;

public class DeleteCategory
{
    public CategoryService _servicio;

    public DeleteCategory(CategoryService servicio)
    {
        _servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));
    }
    public void Ejecutar()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔════════════════════════════════════╗");
        Console.WriteLine("║       🗑️ ELIMINAR CATEGORÍA       ║");
        Console.WriteLine("╚════════════════════════════════════╝");
        Console.ResetColor();

        Console.Write("\n📌 ID de la categoría a eliminar: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            _servicio.Delete(id);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ Categoría eliminada con éxito.");
            Console.ResetColor();

        }
    }
}
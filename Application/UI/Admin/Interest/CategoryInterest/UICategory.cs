using CampusLove.Application.Services;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Application.UI.Admin.Interest.CategoryInterest;

public class UICategory
{
     private readonly CategoryService _servicio;

    public UICategory(CategoryService categoryService)
    {
        _servicio = categoryService;
    }
    public void GestionarCategories()
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║         ⋆｡‧˚ʚ🍒 Menú de Categorías ɞ˚‧｡⋆       ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" ⤜♡→ 1. Ver todas las categorías       📚");
            Console.WriteLine(" ⤜♡→ 2. Agregar una nueva categoría    ✨");
            Console.WriteLine(" ⤜♡→ 3. Actualizar información        📋");
            Console.WriteLine(" ⤜♡→ 4. Eliminar una categoría          💔");
            Console.WriteLine(" ⤜♡→ 0. Volver al menú principal      ↩️");
            Console.ResetColor();
            Console.Write("\n📝 Elige una opción: ");
            var opcion = Console.ReadLine();
            Console.Clear();
            switch (opcion)
            {
                case "1":
                    _servicio.ShowAll();
                    Pausar();
                    break;
                case "2":
                    var crear = new CreateCategory(_servicio);
                    crear.Ejecutar();
                    Pausar();
                    break;
                case "3":
                    var actualizar = new UpdateCategory(_servicio);
                    actualizar.Ejecutar();
                    Pausar();
                    break;
                case "4":
                    var eliminar = new DeleteCategory(_servicio);
                    eliminar.Ejecutar();
                    Pausar();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("❌ Opción no válida. Intenta de nuevo.");
                    Pausar();
                    break;
            }
        }

    }

    private void Pausar()
    {
        Console.WriteLine("\nPresiona cualquier tecla para continuar...");
        Console.ReadKey();
    }
}

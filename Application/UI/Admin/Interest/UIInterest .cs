using System;
using CampusLove.Application.Services;
using CampusLove.Domain.Interfaces;
using CampusLove.Application.UI.Admin.Interest.CategoryInterest;

namespace CampusLove.Application.UI.Admin.Interest;

public class UIInterest
{
    private readonly InterestsService _servicio;
    private readonly CategoryService _categoryService;

    public UIInterest(IDbFactory factory)
    {
        var categoryRepo = factory.CreateCategoryRepository();
        var interestRepo = factory.CreateInterestsRepository();
        
        _categoryService = new CategoryService(categoryRepo);
        _servicio = new InterestsService(interestRepo, categoryRepo);

    }

    public void Ejecutar()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("             ⋆｡‧˚ʚ💖ɞ˚‧｡⋆");
            Console.WriteLine("   ⭒❃.✮:▹ Menú de Intereses ◃:✮.❃⭒");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" ⤜♡→ 1. Ver todos los intereses 🏷️");
            Console.WriteLine(" ⤜♡→ 2. Agrega;r un interés nuevo 🏗️");
            Console.WriteLine(" ⤜♡→ 3. Actualizar un interés ✏️");
            Console.WriteLine(" ⤜♡→ 4. Eliminar un interés ❌");
            Console.WriteLine(" ⤜♡→ 5. Categorias de intereses 📂");
            Console.WriteLine(" ⤜♡→ 0. Volver al menú principal ↩️");
            Console.ResetColor();
            var opcion = Console.ReadLine();
            if (string.IsNullOrEmpty(opcion))
            {
                Console.WriteLine("❌ Debes ingresar una opción.");
                Console.ReadKey();
                continue;
            }
            try
            {
                switch (opcion)
                {
                    case "1":
                        _servicio.ShowAll();
                        break;
                    case "2":
                       var crear = new CreateInterest(_servicio);
                        crear.Ejecutar();
                        break;
                    case "3":
                        var actualizar = new UpdateInterest(_servicio);
                        actualizar.Ejecutar();

                        break;
                    case "4":
                        var eliminar = new DeleteInterest(_servicio);
                        eliminar.Ejecutar();
                        break;
                    case "5":
                        var uiCategory = new UICategory(_categoryService);

                        uiCategory.GestionarCategories();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("❌ Opción no válida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }
            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();
        }
    }


}

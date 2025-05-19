using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;
using CampusLove.Domain.Ports;

namespace CampusLove.Application.Services;

public class CategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public void ShowAll()
    {
        var categories = _categoryRepository.GetAll();
        if (categories == null || !categories.Any())
        {
            Console.WriteLine("No categories found.");
            return;
        }
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n╔══════════════════════════════════════╗");
        Console.WriteLine("║     ​💓​ LISTA DE CATEGORIAS ​💓​       ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.ResetColor();
        foreach (var category in categories)
        {
            Console.WriteLine($"ID: {category.id_category}, Category: {category.interest_category}");
        }
        Console.WriteLine(new string('-', 40));
    }
    public void CreateCategory(Category category)
    {
        if (string.IsNullOrWhiteSpace(category.interest_category))
            throw new ArgumentException("👀 El nombre de la categoria no  puede estar vacia 👀");
        _categoryRepository.Create(category);
    }

    public IEnumerable<Category> GetAll()
    {
        var categories = _categoryRepository.GetAll();
        if (categories == null || !categories.Any())
        {
            Console.WriteLine("📭 No hay categorias registradas");
            return Enumerable.Empty<Category>();
        }
        return _categoryRepository.GetAll();
    }

    public Category? GetById(int id)
    {
        var category = _categoryRepository.GetById(id);
        if (category == null)
        {
            Console.WriteLine("❌ Categoria no encontrada.");
            return null;
        }

        return _categoryRepository.GetById(id);
    }
    public void UpdateCategory(Category category)
    {
        if (category == null)
            throw new ArgumentNullException(nameof(category));
        if (string.IsNullOrWhiteSpace(category.interest_category))
            throw new ArgumentException("👀 El nombre de la categoria no puede estar vacia 👀"
            );
        var existingCategory = _categoryRepository.GetById(category.id_category);
        if (existingCategory == null)
        {
            Console.WriteLine("❌ Categoria no encontrada.");
            return;
        }
        _categoryRepository.Update(category);
    }
    public void Delete(int id)
    {
        var category = _categoryRepository.GetById(id);
        if (category == null)
        {
            Console.WriteLine("❌ Categoria no encontrada.");
            return;
        }
        if (string.IsNullOrWhiteSpace(category.interest_category))
        {
            Console.WriteLine("❌ No se puede eliminar una categoria sin nombre.");
            return;
        }
        _categoryRepository.Delete(id);
        Console.WriteLine($"✅ Categoria con ID {id} eliminada.");
    }
}
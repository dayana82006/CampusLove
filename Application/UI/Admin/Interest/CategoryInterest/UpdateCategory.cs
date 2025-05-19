using System;
using CampusLove.Application.Services;
using CampusLove.Domain.Entities;
namespace CampusLove.Application.UI.Admin.Interest.CategoryInterest
{
    public class UpdateCategory
    {
        private readonly CategoryService _service;

        public UpdateCategory(CategoryService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public void Ejecutar()
        {
            var category = new Category();

            Console.Write("ID de la categoría: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("❌ ID inválido.");
                return;
            }
            category.id_category = id;

            Console.Write("Nombre de la categoría: ");
            category.interest_category = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(category.interest_category))
            {
                Console.WriteLine("❌ El nombre no puede estar vacío.");
                return;
            }

            _service.UpdateCategory(category);
            Console.WriteLine("✅ Categoría actualizada con éxito.");
        }
    }
}

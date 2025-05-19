using CampusLove.Application.Services;
using CampusLove.Domain.Entities;

namespace CampusLove.Application.UI.Admin.Interest;

public class UpdateInterest
{
    private readonly InterestsService _service;
    public UpdateInterest(InterestsService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    public void Ejecutar()
    {
        var interest = new Interests();

        Console.Write("ID del interés a actualizar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("❌ ID inválido. Debe ser un número entero.");
            return;
        }

        interest.id_interest = id;

        Console.Write("Nuevo nombre del interés: ");
        interest.interest_name = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(interest.interest_name))
        {
            Console.WriteLine("❌ El nombre no puede estar vacío.");
            return;
        }

        _service.Update(interest);
        Console.WriteLine($"✅ Interés '{interest.interest_name}' actualizado con éxito.");
    }
    
}

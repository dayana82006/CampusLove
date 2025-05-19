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
        var interest = new Interests();

        Console.Write("Nombre del interés: ");
        interest.interest_name = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(interest.interest_name))
        {
            Console.WriteLine("❌ El nombre no puede estar vacío.");
            return;
        }

        _service.Create(interest);
        Console.WriteLine($"✅ Interés '{interest.interest_name}' creado con éxito.");
    }
}

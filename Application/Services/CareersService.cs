using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Application.Services;

public class CareersService
{
    private readonly ICareersRepository _repository;

    public CareersService(ICareersRepository repository)
    {
        _repository = repository;
    }

    public void ShowAll()
    {
        var careers = GetAll();
        if (careers == null || !careers.Any())
        {
            Console.WriteLine("📭 No hay Carreras Registradas ");
            return;
        }
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n╔══════════════════════════════════════╗");
        Console.WriteLine("║         📚 LISTA DE CARRERAS         ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.ResetColor();

        foreach (var career in careers)
        {
            Console.WriteLine($"🆔 ID: {career.id_career} |  📖 Name: {career.career_name}");
        }
        Console.WriteLine(new string('-', 40));
    }
    public void CreateCareer(Careers career)
    {
        if (string.IsNullOrWhiteSpace(career.career_name))
            throw new ArgumentException("👀​ El nombre de la carrera no puede estar vacío 👀​");
        _repository.Create(career);
    }
    public IEnumerable<Careers> GetAll()
    {
        return _repository.GetAll();
    }

    public Careers? GetById(int id)
    {   
        var career = _repository.GetById(id);
        if (career == null)
        {
            Console.WriteLine("❌ Carrera no encontrada.");
            return null;
        }
        return _repository.GetById(id);
    }
    
    public void Update(Careers career)
    {
        if (career == null)
            throw new ArgumentNullException(nameof(career));
        if (string.IsNullOrWhiteSpace(career.career_name))
            throw new ArgumentException("👀​ El nombre de la carrera no puede estar vacío 👀​");
        var existingCareer = _repository.GetById(career.id_career);
        if (existingCareer == null)
        {
            Console.WriteLine("❌ Carrera no encontrada.");
            return;
        }
        _repository.Update(career);
    }
    public void Delete(int id)
    {
        var existingCareer = _repository.GetById(id);
        if (existingCareer == null)
        {
            Console.WriteLine("❌ Carrera no encontrada.");
            return;
        }
        if (existingCareer.career_name == null)
        {
            Console.WriteLine("❌ No se puede eliminar una carrera sin nombre.");
            return;
        }

        _repository.Delete(id);
        Console.WriteLine($"✅ Carrera con ID {id} eliminada con éxito.");
    }
}

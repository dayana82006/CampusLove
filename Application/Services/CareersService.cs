using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Application.Services;

public class CareersService
{
     // Campos privados para los repositorios inyectados
    private readonly ICareersRepository _repository;

    public CareersService(ICareersRepository repository)
    {
        _repository = repository;
    }

    //Metodo para mostrar todas las carreras
    // Se utiliza el método GetAll() del repositorio para obtener la lista de carreras  
    // y luego se imprime en la consola

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

    //Metodo para crear una carrera
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

    //Metodo para obtener una carrera por id
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
    //Metodo para actualizar una carrera
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

    //Metodo para eliminar una carrera
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

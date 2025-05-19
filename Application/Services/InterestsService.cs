using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;
using CampusLove.Domain.Ports;
using System.Collections.Generic;
using System.Linq;

namespace CampusLove.Application.Services;
public class InterestsService
{
    private readonly IInterestsRepository _repository;
    private readonly ICategoryRepository _categoryRepository;

    public InterestsService(IInterestsRepository repository, ICategoryRepository categoryRepository)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
    }

    public void Create(Interests interests)
    {
        if (string.IsNullOrWhiteSpace(interests.interest_name))
            throw new ArgumentException("Este espacio no puede estar vacío", nameof(interests));

        var category = _categoryRepository.GetById(interests.id_category);
        if (category == null)
            throw new ArgumentException("La categoría no existe", nameof(interests));

        _repository.Create(interests);
    }

    public void Update(Interests interests)
    {
        if (string.IsNullOrWhiteSpace(interests.interest_name))
            throw new ArgumentException("Este espacio no puede estar vacío", nameof(interests));

        var category = _categoryRepository.GetById(interests.id_category);
        if (category == null)
            throw new ArgumentException("La categoría no existe", nameof(interests));

        _repository.Update(interests);
    }

    public void Delete(int id)
    {
        var interest = _repository.GetById(id);
        if (interest == null)
            throw new ArgumentException("El interés no existe", nameof(id));

        _repository.Delete(id);
    }

    public void ShowAll()
    {
        var interests = _repository.GetAll();
        if (interests == null || !interests.Any())
        {
            Console.WriteLine("📭 No hay intereses registrados.");
            return;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n╔══════════════════════════════════════╗");
        Console.WriteLine("║          🌍 LISTA DE INTERESES       ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.ResetColor();

        foreach (var interest in interests)
        {
            Console.WriteLine($"ID: {interest.id_interest}, Name: {interest.interest_name}");
        }

        Console.WriteLine(new string('-', 40));
    }

    public IEnumerable<Interests> GetAll() => _repository.GetAll().ToList();

    public IEnumerable<InterestsCategory> GetAllInterestsCategory() =>
        _repository.GetAllInterestsCategory().ToList();

    public InterestsCategory? GetInterestsCategoryById(int id) =>
        _repository.GetInterestsCategoryById(id);

    public Interests? GetById(int id_interest) => _repository.GetById(id_interest);
}

using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Application.Services
{
    public class GendersService
    {
        // Campos privados
        private readonly IGendersRepository _repository;

        // Constructor
        public GendersService(IGendersRepository repository)
        {
            _repository = repository;
        }

        // Método para mostrar todas los generos
        public IEnumerable<Genders> GetAll()
        {
            return _repository.GetAll();
        }

        // Método para obtener por id
        public Genders? GetById(int id)
        {
            return _repository.GetById(id);
        }

        // Método para mostrar todos los generos
        public void ShowAll()
        {
            var genders = GetAll();
            if (genders == null || !genders.Any())
            {
                Console.WriteLine("📭 No hay géneros registrados.");
                return;
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔══════════════════════════════════════╗");
            Console.WriteLine("║          🌈 LISTA DE GÉNEROS         ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
            foreach (var gender in genders)
            {
                Console.WriteLine($"🆔 ID: {gender.id_gender} |  📖 Name: {gender.genre_name}");
            }
            Console.WriteLine(new string('-', 40));
        }

        // Método para crear un genero
        public void CreateGender(Genders genders)
        {
            if (string.IsNullOrWhiteSpace(genders.genre_name))
                throw new ArgumentException("👀​ El nombre del género no puede estar vacío 👀​");
            _repository.Create(genders);
        }

        //Metodo para actualizar genero 
        public void Update(Genders gender)
        {

            if (gender == null)
                throw new ArgumentNullException(nameof(gender));
            if (string.IsNullOrWhiteSpace(gender.genre_name))
                throw new ArgumentException("👀​ El nombre de la carrera no puede estar vacío 👀​");
            var existingGender = _repository.GetById(gender.id_gender);
            if (existingGender == null)
            {
                Console.WriteLine("❌ Carrera no encontrada.");
                return;
            }
            _repository.Update(gender);
        }


        //metodo para eliminar genero 
        public void Delete(int id)
        {
            var existingGender = _repository.GetById(id);
            if (existingGender == null)
            {
                Console.WriteLine("❌ Género no encontrado.");
                return;
            }
            if (existingGender.genre_name == null)
            {
                Console.WriteLine("❌ El nombre del género no puede estar vacío.");
                return;
            }
            _repository.Delete(id);
            Console.WriteLine($"✅ Género '{existingGender.genre_name}' eliminado con éxito.");
        }
    }
}

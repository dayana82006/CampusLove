using System;
using System.Linq;

using CampusLove.Domain.Entities;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.User
{
    public class CreateUser
    {
        private readonly UserService _servicio;
        private readonly GendersService _genderService;
        private readonly CareersService _careerService;
        private readonly AddressesService _addressService;

        public CreateUser(UserService servicio, GendersService genderService, CareersService careerService, AddressesService addressService)
        {
            _servicio = servicio;
            _genderService = genderService;
            _careerService = careerService;
            _addressService = addressService;
        }

        public void Ejecutar()
        {
            var user = new Users();
            Console.Clear();
            Console.WriteLine("♥♥♥♥♥♥♥ CREAR USUARIO ♥♥♥♥♥♥♥");

            // Nombre
            Console.Write("Nombre: ");
            user.first_name = Console.ReadLine()?.Trim() ?? string.Empty;
            while (string.IsNullOrWhiteSpace(user.first_name) || ContieneNumeros(user.first_name))
            {
                Console.Write("❌ Nombre inválido (no debe contener números). Ingrese nuevamente: ");
                user.first_name = Console.ReadLine()?.Trim() ?? string.Empty;
            }

            // Apellido
            Console.Write("Apellido: ");
            user.last_name = Console.ReadLine()?.Trim() ?? string.Empty;
            while (string.IsNullOrWhiteSpace(user.last_name) || ContieneNumeros(user.last_name))
            {
                Console.Write("❌ Apellido inválido (no debe contener números). Ingrese nuevamente: ");
                user.last_name = Console.ReadLine()?.Trim() ?? string.Empty;
            }

            // Email
            Console.Write("Email: ");
            user.email = Console.ReadLine()?.Trim() ?? string.Empty;

            var (valido, mensaje) = EsEmailValido(user.email);
            while (!valido || _servicio.ExisteEmail(user.email))
            {
                if (!valido)
                {
                    Console.WriteLine($"❌ {mensaje}");
                }
                else if (_servicio.ExisteEmail(user.email))
                {
                    Console.WriteLine("❌ El email ya está registrado. Intente con otro.");
                }

                Console.Write("Ingrese un nuevo email: ");
                user.email = Console.ReadLine()?.Trim() ?? string.Empty;
                (valido, mensaje) = EsEmailValido(user.email);
            }

            // Contraseña
            Console.Write("Contraseña: ");
            user.password = Console.ReadLine()?.Trim() ?? string.Empty;
            while (string.IsNullOrWhiteSpace(user.password) || user.password.Length < 8)
            {
                Console.Write("❌ Contraseña inválida (mínimo 8 caracteres). Ingrese nuevamente: ");
                user.password = Console.ReadLine()?.Trim() ?? string.Empty;
            }

            // Edad
            Console.Write("Edad: ");
            int age;
            while (!int.TryParse(Console.ReadLine(), out age) || age < 18 || age > 120)
            {
                Console.Write("❌ Edad inválida. Debe ser un número entre 18 y 120. Ingrese nuevamente: ");
            }
            user.age = age;

            // GÉNERO
            var generos = _genderService.GetAll();
            Console.WriteLine("Seleccione su género:");
            foreach (var g in generos)
                Console.WriteLine($"{g.id_gender}. {g.genre_name}");
            int id_gender;
            while (true)
            {
                Console.Write("Opción: ");
                if (int.TryParse(Console.ReadLine(), out id_gender) && generos.Any(g => g.id_gender == id_gender))
                    break;
                Console.WriteLine("❌ Opción inválida. Ingrese una opción válida.");
            }
            user.id_gender = id_gender;

            // CARRERA
            var carreras = _careerService.GetAll();
            Console.WriteLine("Seleccione su carrera:");
            foreach (var c in carreras)
                Console.WriteLine($"{c.id_career}. {c.career_name}");
            int id_career;
            while (true)
            {
                Console.Write("Opción: ");
                if (int.TryParse(Console.ReadLine(), out id_career) && carreras.Any(c => c.id_career == id_career))
                    break;
                Console.WriteLine("❌ Opción inválida. Ingrese una opción válida.");
            }
            user.id_career = id_career;

            // DIRECCIÓN
            var direcciones = _addressService.GetAll();
            Console.WriteLine("Seleccione su dirección:");
            foreach (var d in direcciones)
                Console.WriteLine($"{d.id_address}. {d.id_city}");
            int id_address;
            while (true)
            {
                Console.Write("Opción: ");
                if (int.TryParse(Console.ReadLine(), out id_address) && direcciones.Any(d => d.id_address == id_address))
                    break;
                Console.WriteLine("❌ Opción inválida. Ingrese una opción válida.");
            }
            user.id_address = id_address;

            // Frase de perfil
            Console.Write("Frase de perfil: ");
            user.profile_phrase = Console.ReadLine()?.Trim() ?? string.Empty;

            // Crear usuario
            _servicio.CrearUser(user);
            Console.WriteLine("✅ Usuario creado con éxito.");
        }

        private bool ContieneNumeros(string texto)
        {
            return texto.Any(char.IsDigit);
        }

        private (bool, string) EsEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return (false, "El email no puede estar vacío.");

            if (!email.Contains("@"))
                return (false, "El email debe contener '@'.");

            if (!email.Contains("."))
                return (false, "El email debe contener al menos un punto '.'.");

            return (true, string.Empty);
        }
    }
}

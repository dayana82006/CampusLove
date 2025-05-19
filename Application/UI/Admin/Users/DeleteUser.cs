using System;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.User
{
    public class DeleteUser
    {
        private readonly UserService _servicio;

        public DeleteUser(UserService servicio)
        {
            _servicio = servicio;
        }

        public void Ejecutar()
        {
            Console.Clear();
            Console.WriteLine("🗑️ Eliminar usuario");

            Console.Write("Ingrese el ID del usuario a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("❌ ID no válido.");
                return;
            }

            var usuario = _servicio.ObtenerPorId(id);
            if (usuario == null)
            {
                Console.WriteLine("❌ Usuario no encontrado.");
                return;
            }

            Console.WriteLine($"¿Está segura/o de eliminar al usuario {usuario.first_name} {usuario.last_name}? (s/n): ");
            string confirmacion = Console.ReadLine()?.ToLower();

            if (confirmacion == "s")
            {
                _servicio.Eliminar(id);
                Console.WriteLine("✅ Usuario eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("❎ Operación cancelada.");
            }

            Console.ReadKey();
        }
    }
}

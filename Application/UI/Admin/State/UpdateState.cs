using System;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.Admin.State
{
    public class UpdateState
    {
        private readonly StateService _servicio;

        public UpdateState(StateService servicio)
        {
            _servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));
        }

        public void Ejecutar()
        {
            Console.Write("🔄 ID del estado a actualizar: ");
            
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("❌ ID no válido. Debe ser un número entero.");
                return;
            }

            Console.Write("📝 Nuevo nombre del estado: ");
            string nuevoNombre = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(nuevoNombre))
            {
                Console.WriteLine("❌ El nombre no puede estar vacío.");
                return;
            }

            bool actualizado = _servicio.UpdateState(id, nuevoNombre);

            if (actualizado)
            {
                Console.WriteLine("✅ Estado actualizado con éxito.");
            }
            else
            {
                Console.WriteLine("❌ No se encontró un estado con ese ID.");
            }
        }
    }
}

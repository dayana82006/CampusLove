using System;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.Admin.State
{
    public class DeleteState
    {
        private readonly StateService _servicio;

        public DeleteState(StateService servicio)
        {
            _servicio = servicio ?? throw new ArgumentNullException(nameof(servicio));
        }

        public void Ejecutar()
        {
            Console.Write("Ingrese el ID del estado a eliminar: ");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;

            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("❌ ID inválido. Debe ser un número entero.");
                return;
            }

            bool eliminado = _servicio.DeleteState(id); // 👈 Validación del resultado

            if (eliminado)
            {
                Console.WriteLine("✅ Estado eliminado exitosamente.");
            }
            else
            {
                Console.WriteLine("❌ No se encontró un estado con ese ID.");
            }
        }
    }
}

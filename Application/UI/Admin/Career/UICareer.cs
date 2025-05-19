using CampusLove.Application.Services;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Application.UI.Admin.Career;

public class UICareer
{
    private readonly CareersService _servicio;

    public UICareer(IDbFactory factory)
    {
        _servicio = new CareersService(factory.CreateCareersRepository());
    }

    public void GestionarCareers()
    {
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║         ⋆｡‧˚ʚ🍒 Menú de Carreras ɞ˚‧｡⋆       ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.ResetColor();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" ⤜♡→ 1. Ver todas las carreras       🎓");
            Console.WriteLine(" ⤜♡→ 2. Agregar una nueva carrera    ✨");
            Console.WriteLine(" ⤜♡→ 3. Actualizar información        📋");
            Console.WriteLine(" ⤜♡→ 4. Eliminar una carrera          💔");
            Console.WriteLine(" ⤜♡→ 0. Volver al menú principal      ↩️");
            Console.ResetColor();

            Console.Write("\n📝 Elige una opción: ");
            var opcion = Console.ReadLine();

            Console.Clear();
            switch (opcion)
            {
                case "1":
                    _servicio.ShowAll();
                    Pausar();
                    break;
                case "2":
                    var crear = new CreateCareer(_servicio);
                    crear.Ejecutar();
                    Pausar();
                    break;
                case "3":
                    var actualizar = new UpdateCareer(_servicio);
                    actualizar.Ejecutar();
                    Pausar();
                    break;
                case "4":
                    var eliminar = new DeleteCareer(_servicio);
                    eliminar.Ejecutar();
                    Pausar();
                    break;
                case "0":
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Opción no válida. Intenta de nuevo.");
                    Console.ResetColor();
                    Thread.Sleep(1500);
                    break;
            }
        }
    }

    private void Pausar()
    {
        Console.WriteLine("\nPresiona cualquier tecla para continuar...");
        Console.ReadKey();
    }
}

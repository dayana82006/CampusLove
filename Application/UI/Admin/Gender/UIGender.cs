using CampusLove.Application.Services;

namespace CampusLove.Application.UI.Admin.Gender;

public class UIGender
{
    private readonly GendersService _service;
    public UIGender(GendersService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }
   
    public void GestionarGenders()
    {
        while (true)
        {
            
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔════════════════════════════════════╗");
        Console.WriteLine("║         👤 GÉNERO ADMIN           ║");
        Console.WriteLine("╚════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine("\n1.Listar géneros");
        Console.WriteLine("2. Actualizar género");
        Console.WriteLine("3. Eliminar género");
        Console.WriteLine("4. Crear género");
        Console.WriteLine("0. Volver al menú principal");
        Console.Write("\n📝Selecciona una opción: ");
          
            var opcion = Console.ReadLine();
            Console.Clear();
            switch (opcion)
            {
                case "1":
                 _service.ShowAll();
                    Pausar();
                    break;
                case "2":
                   var actualizar = new UpdateGender(_service);
                    actualizar.Ejecutar();
                    Pausar();
                    break;
                case "3":
                    var eliminar = new DeleteGender(_service);
                    eliminar.Ejecutar();
                    Pausar();
                    break;
                    
                case "4":
                
                    var crear = new Creategender(_service);
                    crear.Ejecutar();
                    Pausar();
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

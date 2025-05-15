
namespace CampusLove.Domain.Entities;
public class Intereses
{
    public int Id { get; set; }
    public string Nombre { get; set; }

    public List<UsuarioIntereses> UsuarioIntereses { get; set; } = new();
}


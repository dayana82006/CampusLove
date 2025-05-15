using CampusLove.Domain.Entities;
namespace CampusLove.Domain.Entities;

public class UsuarioIntereses
{
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    public int InteresesId { get; set; }
    public Intereses Intereses { get; set; }
}



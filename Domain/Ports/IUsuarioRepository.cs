using CampusLove.Domain.Entities;

namespace CampusLove.Domain.Ports
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Usuario? ObtenerPorEmail(string email);
        Usuario? ObtenerPorUsuario(string username);
    }
}

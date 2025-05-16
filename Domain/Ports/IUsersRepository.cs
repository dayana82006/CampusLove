using CampusLove.Domain.Entities;

namespace CampusLove.Domain.Interfaces
{
    public interface IUsersRepository
    {
        void Create(Users user);
        Users? GetById(int id);
        IEnumerable<Users> GetAll();
        void Update(Users user);
        void Delete(int id);
        Users? GetByEmail(string email);
        Usuario? GetByUser(string username);
        void Create(Usuario nuevoUsuario);
    }
}

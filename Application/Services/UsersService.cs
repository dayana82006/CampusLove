using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Application.Services
{
   public class UserService
{
    private readonly IUsersRepository _repository;

    public UserService(IUsersRepository repository)
    {
        _repository = repository;
    }

    public void CrearUser(Users user)
    {
        _repository.Create(user);
    }

    public Users? ObtenerPorId(int id)
    {
        return _repository.GetById(id);
    }

    public IEnumerable<Users> ObtenerTodos()
    {
        return _repository.GetAll();
    }

    public void Actualizar(Users user)
    {
        _repository.Update(user);
    }

    public void Eliminar(int id)
    {
        _repository.Delete(id);
    }

    public Users? GetByEmail(string email)
    {
        return _repository.GetByEmail(email);
    }

    public bool ValidarEmailUnico(string email)
    {
        var usuarioExistente = _repository.GetByEmail(email);
        return usuarioExistente == null;
    }

    public bool ExisteEmail(string email)
    {
        var usuarios = _repository.GetAll();
        return usuarios.Any(u => u.email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }
}
}

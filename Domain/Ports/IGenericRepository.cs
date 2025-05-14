using System.Collections.Generic;

namespace CampusLove.Domain.Ports
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> ObtenerTodos(); // usar IEnumerable para más flexibilidad
        T? ObtenerPorId(int id);       // nuevo método obligatorio
        void Crear(T entity);
        void Actualizar(T entity);
        void Eliminar(int id);
    }
}
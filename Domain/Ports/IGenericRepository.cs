using System.Collections.Generic;

namespace CampusLove.Domain.Ports
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> GetAll(); // usar IEnumerable para más flexibilidad
        T? GetById(int id);       // nuevo método obligatorio
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
using System.Collections.Generic;
using CampusLove.Domain.Entities;

namespace CampusLove.Domain.Interfaces
{
    public interface IStatesRepository
    {
        void Insert(States state);
        States? GetById(int id);
        IEnumerable<States> GetAll();
        void Update(States state);
        void Delete(int id);
    }
}

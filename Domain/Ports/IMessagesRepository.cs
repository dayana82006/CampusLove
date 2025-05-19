using System.Collections.Generic;
using CampusLove.Domain.Entities;

namespace CampusLove.Domain.Interfaces
{
    public interface IMessagesRepository
    {
        void Insert(Messages message);
        void Delete(int messageId);
        IEnumerable<Messages> GetAll();
        List<Messages> GetMessagesBetweenUsers(int userId1, int userId2);

    }
}

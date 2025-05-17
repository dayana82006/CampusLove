using System;
using System.Collections.Generic;
using Npgsql;
using CampusLove.Domain.Interfaces;
using CampusLove.Domain.Entities;
using CampusLove.Infrastructure.Pgsql;

namespace CampusLove.Infrastructure.Repositories
{
    public class PgsqlInteractionsRepository : IInteractionsRepository
    {
        private readonly string _connectionString;

        public PgsqlInteractionsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Interactions interaction)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int userId, int targetUserId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Interactions> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Interactions> GetByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Interactions> GetByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Interactions> GetByUserIdAndDate(int userId, DateTime date)
        {
            throw new NotImplementedException();
        }

        public Interactions? GetInteraction(int originId, int targetId)
        {
            throw new NotImplementedException();
        }

        public bool HasInteractedWith(int userId, int targetUserId)
        {
            throw new NotImplementedException();
        }

        public void Update(Interactions interaction)
        {
            throw new NotImplementedException();
        }
    }
}
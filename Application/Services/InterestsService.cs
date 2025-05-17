using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;
using Npgsql; 
using System.Collections.Generic;
using System.Linq;

namespace CampusLove.Application.Services
{
    public class InterestsService
    {
        private readonly IInterestsRepository _repository;
        private readonly string _connStr;  

        public InterestsService(IInterestsRepository repository, string connStr)
        {
            _repository = repository;
            _connStr = connStr;  
        }

        public IEnumerable<Interests> GetAll()
        {
            return _repository.GetAll();
        }

        public Interests? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<InterestsCategory> GetAllInterestsCategory()
        {
            return _repository.GetAllInterestsCategory().ToList();
        }
        public InterestsCategory? GetInterestsCategoryById(int id)
        {
            return _repository.GetInterestsCategoryById(id);
        }

    }
}

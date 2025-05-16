using System;
using CampusLove.Domain.Interfaces;
using CampusLove.Domain.Entities;

namespace CampusLove.Application.Services
{
    public class MatchesService
    {
        private readonly IMatchesRepository _matchesRepository;

        public MatchesService(IMatchesRepository matchesRepository)
        {
            _matchesRepository = matchesRepository;
        }

        public void CreateMatch(int userId1, int userId2)
        {
            var (first, second) = userId1 < userId2 ? (userId1, userId2) : (userId2, userId1);

            if (!_matchesRepository.MatchExists(first, second))
            {
                var match = new Matches
                {
                    id_user1 = first,
                    id_user2 = second,
                    match_date = DateTime.Now
                };

                _matchesRepository.Insert(match);
            }
        }
    }
}

using System.Collections.Generic;
using CampusLove.Domain.Entities;
using CampusLove.Infrastructure.Repositories;

namespace CampusLove.Domain.Interfaces
{
    public interface IUserStatisticsRepository
    {
        UserStatistics GetByUserId(int userId);
        void Add(UserStatistics stats);
        void Update(UserStatistics stats);
        IEnumerable<UserStatistics> GetAll();
        List<UserStatisticsWithUserInfo> GetTopUsersByLikes(int limit = 5);
        List<UserStatisticsWithUserInfo> GetTopUsersByMatches(int limit = 5);
        List<UserStatisticsWithUserInfo> GetMostActiveUsers(int limit = 5);
        List<UserStatisticsWithUserInfo> GetTopUsersByLikeRatio(int limit = 5);
        void EnsureTableExists();
    }
}
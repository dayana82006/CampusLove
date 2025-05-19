namespace CampusLove.Domain.Entities
{
    public class UserStatistics
    {
        public int id_user { get; set; }
        public int received_likes { get; set; } = 0;
        public int received_dislikes { get; set; } = 0;
        public int sent_likes { get; set; } = 0;
        public int sent_dislikes { get; set; } = 0;
        public int total_matches { get; set; } = 0;
        public DateTime last_update { get; set; } = DateTime.Now;
    }

}

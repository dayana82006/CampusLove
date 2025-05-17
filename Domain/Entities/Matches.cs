namespace CampusLove.Domain.Entities
{
    public class Matches
    {
        public int id_match { get; set; }
        public int id_user1 { get; set; }
        public int id_user2 { get; set; }
        public DateTime match_date { get; set; } = DateTime.Now;
    }

}

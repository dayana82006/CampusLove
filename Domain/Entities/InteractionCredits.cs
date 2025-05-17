namespace CampusLove.Domain.Entities
{
    public class InteractionCredits
    {
        public int id_user { get; set; }
        public int available_credits { get; set; } = 10;
        public DateTime last_update_date { get; set; } = DateTime.Now;
    }

}

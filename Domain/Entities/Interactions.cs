namespace CampusLove.Domain.Entities
{
    public class Interactions
    {
        public int id_interaction { get; set; }
        public int id_user_origin { get; set; }
        public int id_user_target { get; set; }
        public string interaction_type { get; set; }
        public DateTime interaction_date { get; set; }
    }

}


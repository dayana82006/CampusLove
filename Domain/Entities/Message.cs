namespace CampusLove.Domain.Entities
{
    public class Messages
    {
        public int id_message { get; set; }
        public int id_user_sender { get; set; }
        public int id_user_receiver { get; set; }
        public string content { get; set; }
        public DateTime send_date { get; set; }
    }
}

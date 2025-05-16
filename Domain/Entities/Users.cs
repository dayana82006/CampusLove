namespace CampusLove.Domain.Entities
{
    public class Users
    {
        public int  id_user { get; set; }
        public string first_name { get; set; } = string.Empty;
        public string last_name { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public int age { get; set; }
        public int id_gender { get; set; }
        public int id_career { get; set; }
        public int id_address { get; set; }
        public string profile_phrase { get; set; } = string.Empty;
    }
}

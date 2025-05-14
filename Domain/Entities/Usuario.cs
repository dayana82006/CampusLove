namespace CampusLove.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
        public string UsuarioName { get; set; } = string.Empty;
        public int Edad { get; set; }
        public int GeneroId { get; set; }
        public int CarreraId { get; set; }
        public int DireccionId { get; set; }
        public string FrasePerfil { get; set; } = string.Empty;
        public int LikesRecibidos { get; set; }
        public int LikesDisponibles { get; set; }
    }
}

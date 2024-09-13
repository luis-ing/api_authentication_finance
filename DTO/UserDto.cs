namespace api_finance.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public bool? Activo { get; set; }
        public string? ImgUrl { get; set; }
        public bool TemaOscuro { get; set; }
    }
}

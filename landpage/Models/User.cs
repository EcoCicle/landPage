namespace WebApplication1.Models
{
    public class User
    {
        public int Id { get; set; }
        public required  string Name { get; set; } = string.Empty;
        public required  string Email { get; set; } = string.Empty;
        public required  string Senha { get; set; } = string.Empty;
        public required  string Data_criacao { get; set; } = string.Empty;
        public required  string Image { get; set; } = string.Empty;
    }
}

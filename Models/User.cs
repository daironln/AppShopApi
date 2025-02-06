namespace AppShop.Models;

public class User
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public List<Comment> Comentarios { get; set; } = new List<Comment>();
    public List<Rating> Valoraciones { get; set; } = new List<Rating>();
    public List<Download> Descargas { get; set; } = new List<Download>();
}
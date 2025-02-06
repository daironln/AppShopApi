namespace AppShop.Models;

public class Rating
{
    public int Id { get; set; }

    public int Estrellas { get; set; }
    public int AppId { get; set; }
    public App App { get; set; }
    public int UsuarioId { get; set; }
    public User Usuario { get; set; }
}
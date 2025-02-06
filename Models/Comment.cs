namespace AppShop.Models;

public class Comment
{
    public int Id { get; set; }
    public string Texto { get; set; }
   
    public int AppId { get; set; }
    public App App { get; set; }
    public int UsuarioId { get; set; }
    public User Usuario { get; set; }
}
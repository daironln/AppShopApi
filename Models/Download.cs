namespace AppShop.Models;

public class Download
{
    public int Id { get; set; }
    public DateTime FechaDescarga { get; set; }
    public int AppId { get; set; }
    public App App { get; set; }
    public int UsuarioId { get; set; }
    public User Usuario { get; set; }
}
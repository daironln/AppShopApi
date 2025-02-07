using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppShop.Models;

public class App
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public ICollection<Comment> Comentarios { get; set; } = new List<Comment>();
    public ICollection<Rating> Valoraciones { get; set; } = new List<Rating>();
    public ICollection<Download> Descargas { get; set; } = new List<Download>();
}
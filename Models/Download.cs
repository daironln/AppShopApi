using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppShop.Models;

public class Download
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime FechaDescarga { get; set; }
    public int AppId { get; set; }
    [ForeignKey("AppId")]
    public App App { get; set; }
    public int UsuarioId { get; set; }
    [ForeignKey("UsuarioId")]
    public User Usuario { get; set; }
}
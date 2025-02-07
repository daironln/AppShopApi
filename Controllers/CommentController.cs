using AppShop.Data;
using AppShop.Models;
using AppShop.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppShop.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly AppDbContext _context;

    public CommentController(AppDbContext context)
    {
        _context = context;
    }

   
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
    {
        return await _context.Comentarios
            .ToListAsync();
    }

  
    [HttpGet("{id}")]
    public async Task<ActionResult<Comment>> GetComment(int id)
    {
        var comentario = await _context.Comentarios
            .FirstOrDefaultAsync(a => a.Id == id);

        if (comentario == null)
        {
            return NotFound();
        }
        return comentario;
    }


    
    [HttpPost("{appId}/comentarios")]
    public async Task<IActionResult> AddComentario(int appId, CommentDTO comentarioDto)
    {
        var app = await _context.Apps.FindAsync(appId);

        if (app == null)
        {
            return NotFound("La app no fue encontrada.");
        }

        var user = await _context.Usuarios.FindAsync(comentarioDto.UsuarioId);

        if (user == null)
        {
            return NotFound("El usuario no existe.");
        }


        Comment comentario = new()
        {
            Texto = comentarioDto.Texto,
            AppId = appId,
            UsuarioId = comentarioDto.UsuarioId
        };

        _context.Comentarios.Add(comentario);
        await _context.SaveChangesAsync();
        return Ok(comentario);
    }
}
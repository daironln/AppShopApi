using AppShop.Data;
using AppShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppShop.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppsController : ControllerBase
{
    private readonly AppDbContext _context;

    public AppsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Apps
    [HttpGet]
    public async Task<ActionResult<IEnumerable<App>>> GetApps()
    {
        return await _context.Apps
            .Include(a => a.Comentarios)
            .Include(a => a.Valoraciones)
            .Include(a => a.Descargas)
            .ToListAsync();
    }

    // GET: api/Apps/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<App>> GetApp(int id)
    {
        var app = await _context.Apps
            .Include(a => a.Comentarios)
            .Include(a => a.Valoraciones)
            .Include(a => a.Descargas)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (app == null)
        {
            return NotFound();
        }
        return app;
    }

    // POST: api/Apps
    [HttpPost]
    public async Task<ActionResult<App>> CreateApp(App app)
    {
        _context.Apps.Add(app);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetApp), new { id = app.Id }, app);
    }

    // POST: api/Apps/{appId}/comentarios
    [HttpPost("{appId}/comentarios")]
    public async Task<IActionResult> AddComentario(int appId, Comment comentario)
    {
        var app = await _context.Apps.FindAsync(appId);
        if (app == null)
        {
            return NotFound("La app no fue encontrada.");
        }
        comentario.AppId = appId;
        _context.Comentarios.Add(comentario);
        await _context.SaveChangesAsync();
        return Ok(comentario);
    }

    // POST: api/Apps/{appId}/valoraciones
    [HttpPost("{appId}/valoraciones")]
    public async Task<IActionResult> AddValoracion(int appId, Rating valoracion)
    {
        var app = await _context.Apps.FindAsync(appId);
        if (app == null)
        {
            return NotFound("La app no fue encontrada.");
        }
        if (valoracion.Estrellas < 1 || valoracion.Estrellas > 5)
        {
            return BadRequest("El numero de estrellas debe estar entre 1 y 5.");
        }
        valoracion.AppId = appId;
        _context.Valoraciones.Add(valoracion);
        await _context.SaveChangesAsync();
        return Ok(valoracion);
    }

    // POST: api/Apps/{appId}/descargas
    [HttpPost("{appId}/descargas")]
    public async Task<IActionResult> AddDescarga(int appId, Download descarga)
    {
        var app = await _context.Apps.FindAsync(appId);
        if (app == null)
        {
            return NotFound("La app no fue encontrada.");
        }
        descarga.AppId = appId;
        descarga.FechaDescarga = DateTime.UtcNow;
        _context.Descargas.Add(descarga);
        await _context.SaveChangesAsync();
        return Ok(descarga);
    }
}
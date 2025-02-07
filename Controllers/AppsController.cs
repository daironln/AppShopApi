using AppShop.Data;
using AppShop.Models;
using AppShop.Models.DTOs;
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

   
    [HttpGet]
    public async Task<ActionResult<IEnumerable<App>>> GetApps()
    {
        return await _context.Apps
            .ToListAsync();
    }

   
    [HttpGet("{id}")]
    public async Task<ActionResult<App>> GetApp(int id)
    {
        var app = await _context.Apps
            .FirstOrDefaultAsync(a => a.Id == id);

        if (app == null)
        {
            return NotFound();
        }
        return app;
    }

    
    [HttpPost]
    public async Task<ActionResult<App>> CreateApp(AppDTO appDto)
    {
        App app = new()
        {
            Nombre = appDto.Nombre,
            Descripcion = appDto.Descripcion
        };

        _context.Apps.Add(app);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetApp), new { id = app.Id }, app);
    }

    
    [HttpPost("usuarios")]
    public async Task<ActionResult<App>> CreateUser(UserDTO userDto)
    {
        User user = new()
        {
            Nombre = userDto.Nombre
        };

        _context.Usuarios.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);
    }

    [HttpGet("usuarios")]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Usuarios
            .ToListAsync();
    }


    [HttpPost("{appId}/valoraciones")]
    public async Task<IActionResult> AddValoracion(int appId, RatingDTO valoracionDto)
    {
        var app = await _context.Apps.FindAsync(appId);
        if (app == null)
        {
            return NotFound("La app no fue encontrada.");
        }
        if (valoracionDto.Estrellas < 1 || valoracionDto.Estrellas > 5)
        {
            return BadRequest("El numero de estrellas debe estar entre 1 y 5.");
        }

        Rating valoracion = new()
        {
            Estrellas = valoracionDto.Estrellas,
            AppId = appId,
            UsuarioId = valoracionDto.UsuarioId
        };

        _context.Valoraciones.Add(valoracion);
        await _context.SaveChangesAsync();
        return Ok(valoracion);
    }

  
    [HttpPost("{appId}/descargas")]
    public async Task<IActionResult> AddDescarga(int appId, DownloadDTO descargaDto)
    {
        var app = await _context.Apps.FindAsync(appId);

        if (app == null)
        {
            return NotFound("La app no fue encontrada.");
        
        }

        Download descarga = new()
        {
            UsuarioId = descargaDto.UsuarioId,
            AppId = appId
        };

        descarga.FechaDescarga = DateTime.UtcNow;

        _context.Descargas.Add(descarga);
        await _context.SaveChangesAsync();
        return Ok(descarga);
    }
}
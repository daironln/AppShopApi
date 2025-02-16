﻿using AppShop.Models;
using Microsoft.EntityFrameworkCore;

namespace AppShop.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<App> Apps { get; set; }
    public DbSet<User> Usuarios { get; set; }
    public DbSet<Comment> Comentarios { get; set; }
    public DbSet<Rating> Valoraciones { get; set; }
    public DbSet<Download> Descargas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<App>().ToTable("App");
        modelBuilder.Entity<User>().ToTable("User");
        modelBuilder.Entity<Comment>().ToTable("Comentario");
        modelBuilder.Entity<Rating>().ToTable("Valoracion");
        modelBuilder.Entity<Download>().ToTable("Descarga");

    }
}
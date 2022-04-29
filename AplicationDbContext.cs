using back_end.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        //para las llaves foranes
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeliculasActores>().HasKey(x => new { x.ActorId, x.PeliculasId });
            modelBuilder.Entity<PeliculasGeneros>().HasKey(x => new { x.PeliculasId, x.GeneroId });
            modelBuilder.Entity<PeliculasCines>().HasKey(x => new { x.CineId, x.PeliculasId });
            base.OnModelCreating(modelBuilder);
        }
        //crear tabla generos en sql server
        public DbSet<Genero> generos { get; set; }
        public DbSet<Actor> Actores { get; set; }

        public DbSet<Cine> Cines { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<PeliculasActores> PeliculasActores { get; set; }
        public DbSet<PeliculasGeneros> PeliculasGeneros { get; set; }
        public DbSet<PeliculasCines> PeliculasCines { get; set; }
    }
}

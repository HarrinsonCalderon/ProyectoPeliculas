using AutoMapper;
using back_end.DTOs;
using back_end.Entidades;
using back_end.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController:ControllerBase
    {
         
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "Peliculas";
        public PeliculasController(
             
            AplicationDbContext context,
            IMapper mapper,
            IAlmacenadorArchivos almacenadorArchivos
            )
        {
 
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] PeliculaCreacionDTO peliculaCreacionDTO) {

            var pelicula = mapper.Map<Pelicula>(peliculaCreacionDTO);
            if (peliculaCreacionDTO.Poster != null)
            {
                pelicula.Poster = await almacenadorArchivos.GuardarArchivo(contenedor, peliculaCreacionDTO.Poster);
            }
            EscribirOrdenActores(pelicula);
            context.Add(pelicula);
            await context.SaveChangesAsync();
            return NoContent();
        }
        private void EscribirOrdenActores(Pelicula pelicula) {

            if (pelicula.PeliculasActores!=null) {
                for (int i = 0; i < pelicula.PeliculasActores.Count; i++)
                {
                    pelicula.PeliculasActores[i].Orden = i;

                }
            }
        }

    }
}

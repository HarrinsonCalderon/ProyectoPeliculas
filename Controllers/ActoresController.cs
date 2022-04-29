using AutoMapper;
using back_end.DTOs;
using back_end.Entidades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
 
using back_end.Utilidades;
namespace back_end.Controllers
{
    [Route("api/actores")]
    [ApiController]
    public class ActoresController: ControllerBase
    {
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "Actores";
        public ActoresController(
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
        public async Task<ActionResult> Post([FromForm] ActorCreacionDTO actorCreacionDTO) {

            var actor = mapper.Map<Actor>(actorCreacionDTO);
            if (actorCreacionDTO.Foto != null) {
             actor.Foto=   await almacenadorArchivos.GuardarArchivo(contenedor,actorCreacionDTO.Foto);
            }
            context.Add(actor);
            await context.SaveChangesAsync();
            return NoContent();

        }
        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get() {
            var queryable = context.Actores.AsQueryable();
            var actores = await queryable.OrderBy(x => x.Nombre).ToArrayAsync();

            return mapper.Map<List<ActorDTO>>(actores);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            var actor = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);
            if (actor == null)
                return NotFound();
            return mapper.Map<ActorDTO>(actor);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] ActorCreacionDTO actorcreacionDTO)
        {

            var actor = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);
            if (actor == null)
                return NotFound();
            actor = mapper.Map(actorcreacionDTO, actor);
            //para la foto
            if (actorcreacionDTO.Foto != null)
            {
                actor.Foto = await almacenadorArchivos.EditarArchivo(contenedor, actorcreacionDTO.Foto,actor.Foto);
            }
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id )
        {
            //int id = 2;
            var actor = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);
            if (actor==null)
            {
                return NotFound();
            }
            context.Remove(actor);
            await context.SaveChangesAsync();
            //para eliminar la foto del actor
            await almacenadorArchivos.BorrarArchivo(actor.Foto,contenedor);
            return NoContent();
        }
    }
}

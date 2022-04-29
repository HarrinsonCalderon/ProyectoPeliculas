using back_end.DTOs;
using back_end.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back_end.Utilidades;

namespace back_end.Controllers
{
  
         [Route("api/generos")]
         [ApiController] 
        public class GenerosController : ControllerBase
        {

        private readonly ILogger<GenerosController> logger;
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;
        public GenerosController(
            ILogger<GenerosController> logger,
            AplicationDbContext context,
            IMapper mapper
            )
        {

            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }
            [HttpGet]
            public async  Task<ActionResult<List<GenereoDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
            {
               var queryable=   context.generos.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionCabecera(queryable);
            var generos = await queryable.OrderBy(x => x.Nombre).Paginar(paginacionDTO).ToArrayAsync();


               //mapear generos a generoDTO
               return mapper.Map<List<GenereoDTO>>(generos);
            }
            
             [HttpGet("{Id:int}")]
             public async Task<ActionResult<GenereoDTO>> Get(int Id )
            {
            var genero = await context.generos.FirstOrDefaultAsync(x => x.Id == Id);
            if (genero == null)
                return NotFound();
            return mapper.Map<GenereoDTO>(genero);
            }

            [HttpPost] 
            public async Task<ActionResult> Post([FromBody] GeneroCreacionDTO generoCreacionDTO)
            {
            var genero = mapper.Map<Genero>(generoCreacionDTO);
            context.Add(genero);
            await context.SaveChangesAsync();
            return NoContent();
            }
            [HttpPut("{id:int}")]
            public async Task<ActionResult> Put(int Id,[FromBody] GeneroCreacionDTO generocreacionDTO)
            {

            var genero = await context.generos.FirstOrDefaultAsync(x => x.Id == Id);
            if (genero == null)
                return NotFound();
            genero = mapper.Map(generocreacionDTO, genero);
            await context.SaveChangesAsync();
            return NoContent();
        }
            [HttpDelete("{id:int}")]
            public async Task<ActionResult> Delete(int id)
            {
            var existe = await context.generos.AnyAsync(x=>x.Id==id);
            if (!existe) {
                return NotFound();
            }
            context.Remove(new Genero() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
            }
        }
        }
    
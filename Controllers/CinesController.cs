using AutoMapper;
using back_end.DTOs;
using back_end.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Controllers
{
    [ApiController]
    [Route("api/cines")]
    public class CinesController:ControllerBase
    {
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;
        
       
        public CinesController(
         AplicationDbContext context,
         IMapper mapper 
         
         )
        {
            this.context = context;
            this.mapper = mapper;
           
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CineCreacionDTO cineCreacionDTO) {
            var cine = mapper.Map<Cine>(cineCreacionDTO);
            context.Add(cine);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet]
        public async Task<ActionResult<List<CineDTO>>> Get()
        {
            var queryable = context.Cines.AsQueryable();
            var cines = await queryable.OrderBy(x => x.Nombre).ToArrayAsync();

            return mapper.Map<List<CineDTO>>(cines);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int Id, [FromBody] CineCreacionDTO cinecreacionDTO)
        {

            var cine = await context.Cines.FirstOrDefaultAsync(x => x.Id == Id);
            if (cine == null)
                return NotFound();
            cine = mapper.Map(cinecreacionDTO, cine);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var actor = await context.Cines.FirstOrDefaultAsync(x => x.Id == id);
            if (actor==null)
            {
                return NotFound();
            }
            context.Remove(actor);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }

}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using saam_webapi.DTOs;
using saam_webapi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace saam_webapi.Controllers
{
    [Route("api/faenas")]
    [ApiController]
    public class FaenaController : ControllerBase
    {
        public readonly ILogger<FaenaController> logger;
        public readonly SAAMDbContext context;
        public readonly IMapper mapper;

        public FaenaController(ILogger<FaenaController> logger,SAAMDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<FaenaDTO>>> Get()
        {
            var faenas = await context.Faenas.ToListAsync();
            return mapper.Map<List<FaenaDTO>>(faenas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FaenaDTO>> Get(int id)
        {
            var faenas = await context.Faenas.FirstOrDefaultAsync(x => x.Id == id);

            if(faenas == null)
            {
                return NotFound();
            }

            return mapper.Map<FaenaDTO>(faenas);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] FaenaCreacionDTO faenaCreacionDTO)
        {
            var faena = mapper.Map<Faena>(faenaCreacionDTO);
            context.Add(faena);
            await context.SaveChangesAsync();
;           return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] FaenaCreacionDTO faenaCreacionDTO)
        {
            var faena = await context.Faenas.FirstOrDefaultAsync(x => x.Id == id);

            if(faena == null)
            {
                return NotFound();
            }

            faena = mapper.Map(faenaCreacionDTO, faena);

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var faena = await context.Faenas.AnyAsync(x => x.Id == id);

            if(!faena)
            {
                return NotFound();
            }

            context.Remove(new Faena() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}

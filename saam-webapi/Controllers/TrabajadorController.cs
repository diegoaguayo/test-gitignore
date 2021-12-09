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
    [Route("api/trabajadores")]
    [ApiController]
    public class TrabajadorController : ControllerBase
    {
        public readonly ILogger<TrabajadorController> logger;
        public readonly SAAMDbContext context;
        public readonly IMapper mapper;

        public TrabajadorController(ILogger<TrabajadorController> logger, SAAMDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TrabajadorDTO>>> Get()
        {
            var trabajadores = await context.Trabajadores.ToListAsync();
            return mapper.Map<List<TrabajadorDTO>>(trabajadores);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TrabajadorDTO>> Get(int id)
        {
            var trabajadores = await context.Trabajadores.FirstOrDefaultAsync(x => x.Id == id);

            if (trabajadores == null)
            {
                return NotFound();
            }

            return mapper.Map<TrabajadorDTO>(trabajadores);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TrabajadorCreacionDTO trabajadorCreacionDTO)
        {
            var trabajador = mapper.Map<Trabajador>(trabajadorCreacionDTO);
            context.Add(trabajador);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] TrabajadorCreacionDTO trabajadorCreacionDTO)
        {
            var trabajador = await context.Trabajadores.FirstOrDefaultAsync(x => x.Id == id);

            if (trabajador == null)
            {
                return NotFound();
            }

            trabajador = mapper.Map(trabajadorCreacionDTO, trabajador);

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var trabajador = await context.Trabajadores.AnyAsync(x => x.Id == id);

            if (!trabajador)
            {
                return NotFound();
            }

            context.Remove(new Trabajador() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
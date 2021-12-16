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
using saam_webapi;

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


    }
}
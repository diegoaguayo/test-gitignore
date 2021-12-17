using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using saam_webapi.DTOs;
using saam_webapi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using saam_webapi;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace saam_webapi.Controllers
{
    [Route("api/cartolas")]
    [ApiController]
    public class CartolaController : ControllerBase
    {
        public readonly ILogger<CartolaController> logger;
        public readonly SAAMDbContext context;
        public readonly IMapper mapper;

        public CartolaController(ILogger<CartolaController> logger, SAAMDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CartolaDTO>>> Get()
        {
            var cartolas = await context.Cartolas.ToListAsync();
            return mapper.Map<List<CartolaDTO>>(cartolas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartolaDTO>> Get(int id)
        {
            var cartolas = await context.Cartolas.FirstOrDefaultAsync(x => x.Id == id);

            if (cartolas == null)
            {
                return NotFound();
            }

            return mapper.Map<CartolaDTO>(cartolas);
        }

        [HttpGet("Filtro/{especialidad:int}/{lista:int}")]
        public async Task<ActionResult<List<CartolaDTO>>> GetEspList(int especialidad,int lista)
        {
            var cartolas = new List<Cartola>();
            if (especialidad != 0 && lista != 0)
            {
                cartolas = await context.Cartolas.Where(x => x.EspecialidadId == especialidad && x.ListaId == lista).ToListAsync();
            }
            else
            {
                if (especialidad == 0 && lista == 0)
                {
                    cartolas = await context.Cartolas.ToListAsync();
                }
                else
                {
                    if (especialidad == 0)
                    {
                        cartolas = await context.Cartolas.Where(x => x.ListaId == lista).ToListAsync();
                    }
                    if (lista == 0)
                    {
                        cartolas = await context.Cartolas.Where(x => x.EspecialidadId == especialidad).ToListAsync();
                    }
                }
            }

            return mapper.Map<List<CartolaDTO>>(cartolas);
        }

    }
}
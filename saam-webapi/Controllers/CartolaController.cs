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

    }
}
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
    [Route("api/lugares")]
    [ApiController]
    public class LugarController : ControllerBase
    {
        public readonly ILogger<LugarController> logger;
        public readonly SAAMDbContext context;
        public readonly IMapper mapper;

        public LugarController(ILogger<LugarController> logger, SAAMDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<LugarDTO>>> Get()
        {
            var lugares = await context.Lugares.ToListAsync();
            return mapper.Map<List<LugarDTO>>(lugares);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LugarDTO>> Get(int id)
        {
            var lugares = await context.Lugares.FirstOrDefaultAsync(x => x.Id == id);

            if (lugares == null)
            {
                return NotFound();
            }

            return mapper.Map<LugarDTO>(lugares);
        }

    }
}
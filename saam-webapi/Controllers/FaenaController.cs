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


    }
}

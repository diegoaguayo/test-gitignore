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
    [Route("api/especialidades")]
    [ApiController]
    public class EspecialidadController : ControllerBase
    {
        public readonly ILogger<EspecialidadController> logger;
        public readonly SAAMDbContext context;
        public readonly IMapper mapper;

        public EspecialidadController(ILogger<EspecialidadController> logger, SAAMDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<EspecialidadDTO>>> Get()
        {
            var especialidades = await context.Especialidades.ToListAsync();
            return mapper.Map<List<EspecialidadDTO>>(especialidades);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EspecialidadDTO>> Get(int id)
        {
            var especialidades = await context.Especialidades.FirstOrDefaultAsync(x => x.Id == id);

            if (especialidades == null)
            {
                return NotFound();
            }

            return mapper.Map<EspecialidadDTO>(especialidades);
        }


    }
}

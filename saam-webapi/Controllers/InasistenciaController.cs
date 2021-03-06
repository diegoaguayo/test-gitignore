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
    [Route("api/inasistencias")]
    [ApiController]
    public class InasistenciaController : ControllerBase
    {
        public readonly ILogger<InasistenciaController> logger;
        public readonly SAAMDbContext context;
        public readonly IMapper mapper;

        public InasistenciaController(ILogger<InasistenciaController> logger, SAAMDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<InasistenciaDTO>>> Get()
        {
            var inasistencias = await context.Inasistencias.ToListAsync();
            return mapper.Map<List<InasistenciaDTO>>(inasistencias);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<InasistenciaDTO>> Get(int id)
        {
            var inasistencias = await context.Inasistencias.FirstOrDefaultAsync(x => x.Id == id);

            if (inasistencias == null)
            {
                return NotFound();
            }

            return mapper.Map<InasistenciaDTO>(inasistencias);
        }


    }
}
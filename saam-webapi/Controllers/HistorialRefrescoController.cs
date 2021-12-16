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
    [Route("api/historialrefresco")]
    [ApiController]
    public class HistorialRefrescoController : ControllerBase
    {
        public readonly ILogger<HistorialRefrescoController> logger;
        public readonly SAAMDbContext context;
        public readonly IMapper mapper;

        public HistorialRefrescoController(ILogger<HistorialRefrescoController> logger, SAAMDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<HistorialRefrescoDTO>>> Get()
        {
            var historialr = await context.HistorialesR.ToListAsync();
            return mapper.Map<List<HistorialRefrescoDTO>>(historialr);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<HistorialRefrescoDTO>> Get(int id)
        {
            var historialr = await context.HistorialesR.FirstOrDefaultAsync(x => x.Id == id);

            if (historialr == null)
            {
                return NotFound();
            }

            return mapper.Map<HistorialRefrescoDTO>(historialr);
        }
    }
}

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
    [Route("api/tipocontratos")]
    [ApiController]
    public class TipocontratoController : ControllerBase
    {
        public readonly ILogger<TipocontratoController> logger;
        public readonly SAAMDbContext context;
        public readonly IMapper mapper;

        public TipocontratoController(ILogger<TipocontratoController> logger, SAAMDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipocontratoDTO>>> Get()
        {
            var tipocontratos = await context.Tipocontratos.ToListAsync();
            return mapper.Map<List<TipocontratoDTO>>(tipocontratos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipocontratoDTO>> Get(int id)
        {
            var tipocontratos = await context.Tipocontratos.FirstOrDefaultAsync(x => x.Id == id);

            if (tipocontratos == null)
            {
                return NotFound();
            }

            return mapper.Map<TipocontratoDTO>(tipocontratos);
        }

    }
}
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
    [Route("api/listas")]
    [ApiController]
    public class ListaController : ControllerBase
    {
        public readonly ILogger<ListaController> logger;
        public readonly SAAMDbContext context;
        public readonly IMapper mapper;

        public ListaController(ILogger<ListaController> logger, SAAMDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ListaDTO>>> Get()
        {
            var listas = await context.Listas.ToListAsync();
            return mapper.Map<List<ListaDTO>>(listas);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ListaDTO>> Get(int id)
        {
            var listas = await context.Listas.FirstOrDefaultAsync(x => x.Id == id);

            if (listas == null)
            {
                return NotFound();
            }

            return mapper.Map<ListaDTO>(listas);
        }

    }
}
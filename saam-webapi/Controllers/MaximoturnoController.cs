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
    [Route("api/maximoturnos")]
    [ApiController]
    public class MaximoturnoController : ControllerBase
    {
        public readonly ILogger<MaximoturnoController> logger;
        public readonly SAAMDbContext context;
        public readonly IMapper mapper;

        public MaximoturnoController(ILogger<MaximoturnoController> logger, SAAMDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<MaximoturnoDTO>>> Get()
        {
            var maximoturnos = await context.Maximoturnos.ToListAsync();
            return mapper.Map<List<MaximoturnoDTO>>(maximoturnos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MaximoturnoDTO>> Get(int id)
        {
            var maximoturnos = await context.Maximoturnos.FirstOrDefaultAsync(x => x.Id == id);

            if (maximoturnos == null)
            {
                return NotFound();
            }

            return mapper.Map<MaximoturnoDTO>(maximoturnos);
        }

    }
}
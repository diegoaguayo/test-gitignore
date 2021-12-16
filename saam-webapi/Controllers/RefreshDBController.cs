using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using saam_webapi.DTOs;
using saam_webapi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using saam_webapi.Utilities;
using saam_webapi;

namespace saam_webapi.Controllers
{
    [Route("api/refresh")]
    [ApiController]
    public class RefreshDBController : ControllerBase
    {
        public readonly ILogger<RefreshDBController> logger;
        public readonly SAAMDbContext SAAMcontext;
        public readonly IMapper mapper;

        public RefreshDBController(ILogger<RefreshDBController> logger, SAAMDbContext SAAMcontext, IMapper mapper)
        {
            this.logger = logger;
            this.SAAMcontext = SAAMcontext;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            return await RefreshDb.RefreshAll(SAAMcontext);
        }

        [HttpGet("ATI")]
        public async Task<ActionResult<string>> RefreshATI()
        {
            return await RefreshDb.RefreshATI(SAAMcontext);
        }

        [HttpGet("ITI")]
        public async Task<ActionResult<string>> RefreshITI()
        {
            return await RefreshDb.RefreshITI(SAAMcontext);
        }

        [HttpGet("STI")]
        public async Task<ActionResult<string>> RefreshSTI()
        {
            return await RefreshDb.RefreshSTI(SAAMcontext);
        }

        [HttpGet("SVTI")]
        public async Task<ActionResult<string>> RefreshSVTI()
        {
            return await RefreshDb.RefreshSVTI(SAAMcontext);
        }

    }
}

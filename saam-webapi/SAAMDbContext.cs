using Microsoft.EntityFrameworkCore;
using saam_webapi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saam_webapi
{
    public class SAAMDbContext : DbContext
    {
        public SAAMDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Especialidad> Especialidades { get; set; }
    }
}

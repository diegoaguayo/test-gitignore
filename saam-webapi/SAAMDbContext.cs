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

        public DbSet<Terminal> Terminales { get; set; }
        public DbSet<Lugar> Lugares { get; set; }
        public DbSet<Tipocontrato> Tipocontratos { get; set; }
        public DbSet<Faena> Faenas { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Maximoturno> Maximoturnos { get; set; }
        public DbSet<Lista> Listas { get; set; }
        public DbSet<Cartola> Cartolas { get; set; }
        public DbSet<Trabajador> Trabajadores { get; set; }
        public DbSet<Inasistencia> Inasistencias { get; set; }

    }
}

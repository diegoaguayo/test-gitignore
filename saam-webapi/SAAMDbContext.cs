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
        public SAAMDbContext(DbContextOptions<SAAMDbContext> options) : base(options)
        {
        }

        public DbSet<Lugar> Lugares { get; set; }
        public DbSet<Tipocontrato> Tipocontratos { get; set; }
        public DbSet<Faena> Faenas { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Maximoturno> Maximoturnos { get; set; }
        public DbSet<Lista> Listas { get; set; }
        public DbSet<Cartola> Cartolas { get; set; }
        public DbSet<Trabajador> Trabajadores { get; set; }
        public DbSet<Inasistencia> Inasistencias { get; set; }
        public DbSet<HistorialRefresco> HistorialesR { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tipocontrato>().HasData(

                new Tipocontrato { Id = 1, Nombre = 'B', Terminal = "Nominacion_ITI" },
                new Tipocontrato { Id = 2, Nombre = 'R', Terminal = "Nominacion_ITI" },
                new Tipocontrato { Id = 3, Nombre = 'E', Terminal = "Nominacion_ITI" },
                new Tipocontrato { Id = 4, Nombre = 'Q', Terminal = "Nominacion_ITI" },

                new Tipocontrato { Id = 5, Nombre = 'B', Terminal = "Nominacion_ATI" },
                new Tipocontrato { Id = 6, Nombre = 'R', Terminal = "Nominacion_ATI" },
                new Tipocontrato { Id = 7, Nombre = 'E', Terminal = "Nominacion_ATI" },
                new Tipocontrato { Id = 8, Nombre = 'Q', Terminal = "Nominacion_ATI" },
                new Tipocontrato { Id = 9, Nombre = 'M', Terminal = "Nominacion_ATI" },

                new Tipocontrato { Id = 10, Nombre = 'B', Terminal = "Nominacion_STI" },
                new Tipocontrato { Id = 11, Nombre = 'R', Terminal = "Nominacion_STI" },
                new Tipocontrato { Id = 12, Nombre = 'E', Terminal = "Nominacion_STI" },
                new Tipocontrato { Id = 13, Nombre = 'Q', Terminal = "Nominacion_STI" },

                new Tipocontrato { Id = 14, Nombre = 'B', Terminal = "Nominacion_SVTI" },
                new Tipocontrato { Id = 15, Nombre = 'R', Terminal = "Nominacion_SVTI" },
                new Tipocontrato { Id = 16, Nombre = 'E', Terminal = "Nominacion_SVTI" },
                new Tipocontrato { Id = 17, Nombre = 'Q', Terminal = "Nominacion_SVTI" }

            );

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saam_webapi.Entities
{
    public class Inasistencia
    {
        public  int Id { get; set; }
        public string Observacion { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }
        public int Dias { get; set; }
        public int TrbajadorId { get; set; }
        public Trabajador Trabajador { get; set; }
        public int EspecialidadId { get; set; }
        public Especialidad Especialidad { get; set; }

    }
}

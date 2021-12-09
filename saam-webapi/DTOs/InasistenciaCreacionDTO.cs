using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saam_webapi.DTOs
{
    public class InasistenciaCreacionDTO
    {
        public string Observacion { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }
        public int Dias { get; set; }
        public int TrabajadorId { get; set; }
        public int EspecialidadId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saam_webapi.DTOs
{
    public class InasistenciaDTO
    {
        public string Tipo { get; set; }
        public string Observacion { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }
        public int Dias { get; set; }
        public string Rut { get; set; }
        public int EspecialidadId { get; set; }
        public string Terminal { get; set; }
    }
}

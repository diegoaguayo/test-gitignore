using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saam_webapi.Entities
{
    public class Trabajador
    {
        public int Id { get; set; }
        public string Rut { get; set; }
        public string Nombres { get; set; }
        public string Papellido { get; set; }
        public string Sapellido { get; set; }
        public string Terminal { get; set; }
        public int TipocontratoId { get; set; }
        public Tipocontrato Tipocontrato { get; set; }
        public int EspecialidadId { get; set; }
        public Especialidad Especialidad { get; set; }

    }
}

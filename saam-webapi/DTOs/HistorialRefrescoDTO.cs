using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saam_webapi.DTOs
{
    public class HistorialRefrescoDTO
    {
        public string Terminal { get; set; }
        public DateTime Fecha { get; set; }
        public int Creado { get; set; }
        public int Editado { get; set; }
        public int Eliminado { get; set; }
        public string Maestro { get; set; }

    }
}

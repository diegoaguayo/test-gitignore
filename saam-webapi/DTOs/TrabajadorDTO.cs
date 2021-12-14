using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saam_webapi.DTOs
{
    public class TrabajadorDTO
    {
        public string Rut { get; set; }
        public string Pnombre { get; set; }
        public string Snombre { get; set; }
        public string Papellido { get; set; }
        public string Sapellido { get; set; }
        public int TipocontratoId { get; set; }
        public int TerminalId { get; set; }
        public int EspecialidadId { get; set; }
    }
}

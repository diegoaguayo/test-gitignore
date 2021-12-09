using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saam_webapi.DTOs
{
    public class EspecialidadDTO
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public int CostoTordinario { get; set; }
        public int CostoTextraordinario { get; set; }
        public int FaenaId { get; set; }

    }
}

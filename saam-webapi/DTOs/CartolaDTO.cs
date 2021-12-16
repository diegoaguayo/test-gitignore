using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saam_webapi.DTOs
{
    public class CartolaDTO
    {
        public string Rut { get; set; }
        public int Posicion {get; set;}
        public int Puerta { get; set; }
        public int ListaId { get; set; }
        public int EspecialidadId { get; set; }
        public string Terminal { get; set; }

    }
}
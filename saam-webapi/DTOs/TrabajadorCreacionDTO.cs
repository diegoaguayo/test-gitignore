﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saam_webapi.DTOs
{
    public class TrabajadorCreacionDTO
    {
        public string Rut { get; set; }
        public string Nombres { get; set; }
        public string Papellido { get; set; }
        public string Sapellido { get; set; }
        public char Planta { get; set; }
        public int Pfono { get; set; }
        public int Sfono { get; set; }
        public int EspecialidadId { get; set; }
    }
}
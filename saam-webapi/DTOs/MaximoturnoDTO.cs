using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saam_webapi.DTOs
{
    public class MaximoturnoDTO
    {
        public string Saldo { get; set; }
        public int Valor { get; set; }
        public int TipocontratoId { get; set; }
        public int EspecialidadId { get; set; }
        public string Terminal { get; set; }


    }
}
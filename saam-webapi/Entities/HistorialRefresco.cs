using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saam_webapi.Entities
{
    public class HistorialRefresco
    {
        public int Id { get; set; }
        public string Terminal { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public string Maestro { get; set; }

    }
}

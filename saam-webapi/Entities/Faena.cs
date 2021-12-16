using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saam_webapi.Entities
{
    public class Faena
    {
        public int Id { get; set; }
        public int Origen { get; set; }
        public string Nombre { get; set; }
        public string Terminal { get; set; }
    }
}

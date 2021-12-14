using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saam_webapi.Entities
{
    public class Lista
    {
        public  int Id { get; set; }
        public string Nombre { get; set; }
        public int Nlista { get; set; }
        public int TerminalId { get; set; }
        public Terminal Terminal { get; set; }

    }
}

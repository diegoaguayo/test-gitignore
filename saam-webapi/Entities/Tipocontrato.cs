using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saam_webapi.Entities
{
    public class Tipocontrato
    {
        public int Id { get; set; }
        public char Nombre { get; set; }
        public int TerminalId { get; set; }
        public Terminal Terminal { get; set; }

    }
}
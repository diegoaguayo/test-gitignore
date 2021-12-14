using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saam_webapi.Entities
{
    public class Cartola
    {
        public  int Id { get; set; }
        public string Rut { get; set; }
        public int Posicion {get; set;}
        public int Puerta { get; set; }
        public int ListaId { get; set; }
        public Lista Lista { get; set; }
        public int EspecialidadId { get; set; }
        public Especialidad Especialidad { get; set; }
        public int TerminalId { get; set; }
        public Terminal Terminal { get; set; }

    }
}
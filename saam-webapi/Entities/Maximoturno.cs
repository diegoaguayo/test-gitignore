using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saam_webapi.Entities
{
    public class Maximoturno
    {
        public int Id { get; set; }
        public string Saldo { get; set; }
        //Valor = Numeroturno
        public int Valor { get; set; }
        public int TipocontratoId { get; set; }
        public Tipocontrato Tipocontrato { get; set; }
        public int EspecialidadId { get; set; }
        public Especialidad Especialidad { get; set; }
        public int TerminalId { get; set; }
        public Terminal Terminal { get; set; }

    }
}

using System;
using System.Collections.Generic;

namespace proyectoIPC.Models
{
    public partial class SalaEspera
    {
        public int Id { get; set; }
        public int? IdJugador { get; set; }
        public string NombreTorneo { get; set; }
        public int? EnEspera { get; set; }
    }
}

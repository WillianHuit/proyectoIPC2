using System;
using System.Collections.Generic;

namespace proyectoIPC.Models
{
    public partial class Torneo
    {
        public int IdPartida { get; set; }
        public int? JugadorA { get; set; }
        public int? JugadorB { get; set; }
        public int? Ganador { get; set; }
        public int? Ronda { get; set; }
        public int? IdTorneo { get; set; }

        public virtual Usuario GanadorNavigation { get; set; }
        public virtual CrearTorneo IdTorneoNavigation { get; set; }
        public virtual Usuario JugadorANavigation { get; set; }
        public virtual Usuario JugadorBNavigation { get; set; }
    }
}

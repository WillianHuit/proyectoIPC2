using System;
using System.Collections.Generic;

namespace proyectoIPC.Models
{
    public partial class CrearTorneo
    {
        public CrearTorneo()
        {
            Torneo = new HashSet<Torneo>();
        }

        public int IdTorneo { get; set; }
        public string Nombre { get; set; }
        public int? IdCreador { get; set; }
        public int? IdPartida { get; set; }

        public virtual Usuario IdCreadorNavigation { get; set; }
        public virtual Partida IdPartidaNavigation { get; set; }
        public virtual ICollection<Torneo> Torneo { get; set; }
    }
}

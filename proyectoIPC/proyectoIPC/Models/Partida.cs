using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace proyectoIPC.Models
{
    [Table("Partida", Schema = "dbo")]
    public partial class Partida
    {
        public Partida()
        {
            CrearSala = new HashSet<CrearSala>();
            CrearTorneo = new HashSet<CrearTorneo>();
        }

        public int IdPartida { get; set; }
        public int? JugadorA { get; set; }
        public int? JugadorB { get; set; }
        public int? Ganador { get; set; }

        public virtual Usuario GanadorNavigation { get; set; }
        public virtual Usuario JugadorANavigation { get; set; }
        public virtual Usuario JugadorBNavigation { get; set; }
        public virtual ICollection<CrearSala> CrearSala { get; set; }
        public virtual ICollection<CrearTorneo> CrearTorneo { get; set; }
    }
}

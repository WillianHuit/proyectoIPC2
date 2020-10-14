using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectoIPC.Models
{
    [Table("Usuario", Schema = "dbo")]
    public partial class Usuario
    {
        public Usuario()
        {
            CrearSala = new HashSet<CrearSala>();
            CrearTorneo = new HashSet<CrearTorneo>();
            PartidaGanadorNavigation = new HashSet<Partida>();
            PartidaJugadorANavigation = new HashSet<Partida>();
            PartidaJugadorBNavigation = new HashSet<Partida>();
            TorneoGanadorNavigation = new HashSet<Torneo>();
            TorneoJugadorANavigation = new HashSet<Torneo>();
            TorneoJugadorBNavigation = new HashSet<Torneo>();
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Usr { get; set; }
        public string Pass { get; set; }
        public string Pais { get; set; }
        public string Fecha { get; set; }
        public string Correo { get; set; }
        public int? Enlinea { get; set; }

        public virtual ICollection<CrearSala> CrearSala { get; set; }
        public virtual ICollection<CrearTorneo> CrearTorneo { get; set; }
        public virtual ICollection<Partida> PartidaGanadorNavigation { get; set; }
        public virtual ICollection<Partida> PartidaJugadorANavigation { get; set; }
        public virtual ICollection<Partida> PartidaJugadorBNavigation { get; set; }
        public virtual ICollection<Torneo> TorneoGanadorNavigation { get; set; }
        public virtual ICollection<Torneo> TorneoJugadorANavigation { get; set; }
        public virtual ICollection<Torneo> TorneoJugadorBNavigation { get; set; }
    }
}

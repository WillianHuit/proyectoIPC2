using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectoIPC.Models
{
    [Table("CrearSala", Schema = "dbo")]
    public partial class CrearSala
    {
        public int IdSala { get; set; }
        public int? IdCreador { get; set; }
        public int? IdPartida { get; set; }

        public virtual Usuario IdCreadorNavigation { get; set; }
        public virtual Partida IdPartidaNavigation { get; set; }
    }
}

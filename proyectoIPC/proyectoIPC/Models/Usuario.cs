using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectoIPC.Models
{
    [Table("Usuario",Schema ="dbo")]
    public partial class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Nombre")]
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Usr { get; set; }
        [Display(Name = "Nombre de Usuario")]
        public string Pass { get; set; }
        [Display(Name = "Clave de Usuario")]
        [DataType(DataType.Password)]
        public string Pais { get; set; }
        public string Fecha { get; set; }
        public string Correo { get; set; }
        public int? Enlinea { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebitoCredito.Domain.Entidades
{
    /// <summary>
    /// Entidad cliente
    /// </summary>
    [Table("Cliente")]
    public  class Cliente
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClienteId { get; set; }
        public string Contrasena { get; set; }
        public bool Estado { get; set; }

        [ForeignKey("Persona")]
        public int PersonaId { get; set; }
        public virtual Persona Persona { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebitoCredito.Domain.Entidades
{
    [Table("Persona")]
    public  class Persona
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonaId { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public string Edad { get; set; }
        public string Direccion { get; set; }
        public string Identificacion { get; set; }
        public string Telefono { get; set; }
    }
}

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
    /// Entidad cuenta
    /// </summary>
    [Table("Cuenta")]
    public class Cuenta
    {
        [Key]
        public int CuentaId { get; set; }
        public int NumeroCuenta { get; set; }
        /// <summary>
        /// Tipo de cuenta a crear: Ahorro, Corriente
        /// </summary>
        public string Tipo { get; set; }
        public decimal SaldoInicial { get; set; }
        /// <summary>
        /// Estado de la cuenta true o false
        /// </summary>
        public bool Estado { get; set; }
        public decimal MontoDiario { get; set; }

        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebitoCredito.Domain.Entidades
{
    /// <summary>
    /// Entidad movimientos
    /// </summary>
    [Table("Movimientos")]
    public class Movimientos
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovimientoId { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime FechaMovimiento { get; set; }
        /// <summary>
        /// Tipo de movimiento a relizar, deposito o debito
        /// </summary>
        public string TipoMovimiento { get; set; }
        public decimal Valor { get; set; }
        /// <summary>
        /// Monto a realizar
        /// </summary>
        public decimal Saldo { get; set; }

        [ForeignKey("Cuenta")]
        public int CuentaId { get; set; }
        public virtual Cuenta Cuenta { get; set; }
    }
}

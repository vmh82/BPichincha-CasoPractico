using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebitoCredito.Application.Dto
{
    public  class DetalleMovimientoDto
    {
       public int NumeroCuenta { get; set; } 
       public string Tipo { get; set; }
       public decimal SaldoInicial { get; set; }
       public string Movimiento { get; set; }
       public string FechaMovimiento { get; set; }
    }
}

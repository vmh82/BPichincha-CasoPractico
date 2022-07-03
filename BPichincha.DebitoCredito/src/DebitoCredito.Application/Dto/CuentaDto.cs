using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DebitoCredito.Application.Dto
{
    public class CuentaDto
    {
        public int NumeroCuenta { get; set; }
        public string Tipo { get; set; }
        public string Identificacion { get; set; }
        public decimal SaldoInicial { get; set; }
        [JsonIgnore]
        public int ClienteId {get;set;}
        public decimal MontoDiario { get; set; }
    }
}

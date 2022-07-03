using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DebitoCredito.Application.Dto
{
    public class ClienteDto
    {
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public string Edad { get; set; }
        public string Direccion { get; set; }
        public string Identificacion { get; set; }
        public string Telefono { get; set; }
        public string Contrasena { get; set; }
        [JsonIgnore]
        public int ClienteId { get; set; }
    }
}

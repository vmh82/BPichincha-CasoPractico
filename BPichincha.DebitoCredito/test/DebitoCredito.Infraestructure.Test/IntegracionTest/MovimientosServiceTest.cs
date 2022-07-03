using DebitoCredito.Application.Dto;
using DebitoCredito.Application.Util;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DebitoCredito.Infraestructure.Test.Integracion
{
    /// <summary>
    /// Clase para verificar endpoints de integracion movimientos
    /// </summary>
    public class MovimientosServiceTest 
    {
        private HttpClient _httpClient;
        public MovimientosServiceTest()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }

        /// <summary>
        /// Permite verificar consultar el estado de cuenta de un cliente, asociado al endpoint de movimientos
        /// </summary>
        /// <param name="fechaInicio">fecha inicio</param>
        /// <param name="fechaFin">fecha de fin</param>
        /// <param name="identificacion">identificacion de cliente a verificar</param>
        /// <returns></returns>
       [Theory]
       [InlineData("01/01/2022", "07/07/2022", "1724389746")]
        public async Task VerificarEndPointEstadoDeCuentaMovimientos(DateTime fechaInicio, DateTime fechaFin, string identificacion)
        {
            var response = await _httpClient.GetAsync($"api/movimiento/reporte/?fechaInicio={fechaInicio}&fechaFin={fechaFin}&identificacion={identificacion}");
            var stringResult = await response.Content.ReadAsStringAsync();
            Assert.NotNull(stringResult);
            Response<List<EstadoDeCuentaDto>> cuentaDto = JsonConvert.DeserializeObject<Response<List<EstadoDeCuentaDto>>>(stringResult);
            Assert.NotNull(cuentaDto.Mensaje);
            Assert.Contains(cuentaDto.Mensaje.First().Cliente, "Marianela Montalvo");
        }
    }
}


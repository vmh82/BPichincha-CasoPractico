using DebitoCredito.Application.Dto;
using DebitoCredito.Application.Util;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DebitoCredito.Infraestructure.Test.IntegracionTest
{
    /// <summary>
    /// Clase para verificar endpoints de integracion cuenta
    /// </summary>
    public  class CuentaServiceTest
    {
        private HttpClient _httpClient;
        public CuentaServiceTest()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }
        /// <summary>
        /// Permite realizar la verificacion del end point de consulta de una cuenta
        /// </summary>
        /// <param name="numeroCuenta">numero de cuenta</param>
        /// <returns></returns>
        [Theory]
        [InlineData("496825")]
        public async Task VerificarEndPointCuentaCliente(string numeroCuenta)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/cuenta/consultar/?numeroCuenta={numeroCuenta}");
            string stringResult = await response.Content.ReadAsStringAsync();
            Assert.NotNull(stringResult);
            Response<CuentaDto> cuentaDto = JsonConvert.DeserializeObject<Response<CuentaDto>>(stringResult);
            Assert.NotNull(cuentaDto.Mensaje.Identificacion);
            Assert.Contains(cuentaDto.Mensaje.Identificacion, "1724389746");
        }
    }
}

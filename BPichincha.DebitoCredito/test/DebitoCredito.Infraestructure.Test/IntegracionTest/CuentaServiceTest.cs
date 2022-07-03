using DebitoCredito.Application.Dto;
using DebitoCredito.Application.Util;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DebitoCredito.Infraestructure.Test.IntegracionTest
{
    public  class CuentaServiceTest
    {
        private HttpClient _httpClient;
        public CuentaServiceTest()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }

        [Theory]
        [InlineData("496825")]
        public async Task VerificarEndPointCuentaCliente(string numeroCuenta)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/cuenta/consultar/?identificacion={numeroCuenta}");
            string stringResult = await response.Content.ReadAsStringAsync();
            Assert.NotNull(stringResult);
            Response<CuentaDto> cuentaDto = JsonConvert.DeserializeObject<Response<CuentaDto>>(stringResult);
            Assert.NotNull(cuentaDto.Mensaje.Identificacion);
            Assert.Contains(cuentaDto.Mensaje.Identificacion, "17181920");
        }
    }
}

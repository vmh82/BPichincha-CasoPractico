using DebitoCredito.Application.Dto;
using DebitoCredito.Application.Util;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DebitoCredito.Infraestructure.Test.IntegracionTest
{
    /// <summary>
    /// Clase para verificar endpoints de integracion cliente
    /// </summary>
    public class ClienteServiceTest
    {
        private HttpClient _httpClient;
        public ClienteServiceTest()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }
        /// <summary>
        /// Permite realizar la verificacion del end point de consulta de cliente
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task VerificarEndPointCreacionCliente()
        {
            ClienteDto cliente = new ClienteDto();
            cliente.Nombre = "Juan Osorio";
            cliente.Genero = "Masculino";
            cliente.Edad = "31";
            cliente.Direccion = "13 junio y Equinoccial";
            cliente.Identificacion = "1724389745";
            cliente.Telefono =  "097548956";
            cliente.Contrasena =  "5679";
            string jsonCliente = JsonConvert.SerializeObject(cliente);
            StringContent httpContent = new StringContent(jsonCliente, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync($"api/cliente/crear/", httpContent);
            string stringResult = await response.Content.ReadAsStringAsync();
            Assert.NotNull(stringResult);
            Response<ClienteDto> clienteDto = JsonConvert.DeserializeObject<Response<ClienteDto>>(stringResult);
            Assert.NotNull(clienteDto.Mensaje.Identificacion);
            Assert.Contains(clienteDto.Mensaje.Identificacion, "1724389745");
        }

        /// <summary>
        /// Permite realizar la verificacion del end point de consulta de una cuenta
        /// </summary>
        /// <param name="numeroCuenta">numero de cuenta</param>
        /// <returns></returns>
        [Theory]
        [InlineData("1724389745")]
        public async Task VerificarEndPointConsultaCliente(string identificacion)
        {
            var response = await _httpClient.GetAsync($"api/cliente/consultar/?identificacion={identificacion}");
            var stringResult = await response.Content.ReadAsStringAsync();
            Assert.NotNull(stringResult);
            Response<ClienteDto> clienteDto = JsonConvert.DeserializeObject<Response<ClienteDto>>(stringResult);
            Assert.NotNull(clienteDto.Mensaje);
            Assert.Contains(clienteDto.Mensaje.Nombre, "Juan Osorio");
        }
    }
}

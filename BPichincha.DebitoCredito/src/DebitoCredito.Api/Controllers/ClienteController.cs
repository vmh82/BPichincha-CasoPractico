using DebitoCredito.Application.Dto;
using DebitoCredito.Application.Services.Interfaces;
using DebitoCredito.Application.Util;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
namespace DebitoCredito.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        [Route("Crear")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ClienteDto>))]
        public async Task<IActionResult> Crear(ClienteDto request)
        {
            Response<ClienteDto> response = await _clienteService.CrearCliente(request);
            return StatusCode((int)response.Codigo, response);
        }

        [HttpGet]
        [Route("Consultar")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ClienteDto>))]
        public async Task<IActionResult> Buscar(string identificacion)
        {
            Response<ClienteDto> response = await _clienteService.ConsultarCliente(identificacion);
            return StatusCode((int)response.Codigo, response);
        }

        [HttpPut]
        [Route("Actualizar")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ClienteDto>))]
        public async Task<IActionResult> Actualizar(ClienteDto request)
        {
            Response<ClienteDto> response = await _clienteService.ActualizarCliente(request);
            return StatusCode((int)response.Codigo, response);
        }

        [HttpDelete]
        [Route("Eliminar")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ClienteDto>))]
        public async Task<IActionResult> Eliminar(string identificacion)
        {
            Response<ClienteDto> response = await _clienteService.EliminarCliente(identificacion);
            return StatusCode((int)response.Codigo, response);
        }
    }
}

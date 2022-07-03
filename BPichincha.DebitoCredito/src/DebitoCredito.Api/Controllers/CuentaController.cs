using DebitoCredito.Application.Dto;
using DebitoCredito.Application.Services.Interfaces;
using DebitoCredito.Application.Util;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DebitoCredito.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaService _cuentaService;

        public CuentaController(ICuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        [HttpPost]
        [Route("Crear")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<CuentaDto>))]
        public async Task<IActionResult> Crear(CuentaDto cuenta)
        {
            Response<CuentaDto> response =  await _cuentaService.CrearCuenta(cuenta);
            return StatusCode((int)response.Codigo, response);

        }

        [HttpGet]
        [Route("Consultar")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<CuentaDto>))]
        public async Task<IActionResult> ConsultarCuenta(int numeroCuenta)
        {
            Response<CuentaDto> response = await _cuentaService.ConsultarCuenta(numeroCuenta);
            return StatusCode((int)response.Codigo, response);

        }

        [HttpPut]
        [Route("Actualizar")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<CuentaDto>))]
        public async Task<IActionResult> Actualizar(CuentaDto request)
        {
            Response<CuentaDto> response = await _cuentaService.ActualizarCuenta(request);
            return StatusCode((int)response.Codigo, response);
        }

        [HttpDelete]
        [Route("Eliminar")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<CuentaDto>))]
        public async Task<IActionResult> Eliminar(int numeroCuenta)
        {
            Response<CuentaDto> response = await _cuentaService.EliminarCuenta(numeroCuenta);
            return StatusCode((int)response.Codigo, response);
        }

    }
}

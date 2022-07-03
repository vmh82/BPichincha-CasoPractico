using DebitoCredito.Application.Dto;
using DebitoCredito.Application.Services.Interfaces;
using DebitoCredito.Application.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net.Mime;

namespace DebitoCredito.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly IMovimientoService _movimientoService;


        public MovimientoController(IMovimientoService movimientoService)
        {
            _movimientoService = movimientoService;
        }

        [HttpPost]
        [Route("Crear")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<DetalleMovimientoDto>>))]
        public async Task<IActionResult> Crear(MovimientosDto movimientoDto)
        {
            Response<List<DetalleMovimientoDto>> response = await _movimientoService.AcreditarDebitar(movimientoDto);
            return StatusCode((int)response.Codigo, response);

        }

        [HttpGet()]
        [Consumes(MediaTypeNames.Application.Json)]
        [Route("Reporte")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<EstadoDeCuentaDto>>))]
        public async Task<IActionResult> ConsultarCuenta(DateTime fechaInicio, DateTime fechaFin, string identificacion)
        {

            Response<List<EstadoDeCuentaDto>> response  = await _movimientoService.ConsultarMovimientosPorFecha(fechaInicio, fechaFin, identificacion);
            return StatusCode((int)response.Codigo, response);

        }

        [HttpDelete]
        [Route("Eliminar")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<int>))]
        public async Task<IActionResult> Eliminar(int numeroCuenta)
        {
            Response<int> response = await _movimientoService.EliminarMovimientos(numeroCuenta);
            return StatusCode((int)response.Codigo, response);
        }
    }
}

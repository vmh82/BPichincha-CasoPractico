using DebitoCredito.Application.Constantes;
using DebitoCredito.Application.Dto;
using DebitoCredito.Application.Services.Interfaces;
using DebitoCredito.Application.Util;
using DebitoCredito.Domain.Entidades;
using DebitoCredito.Domain.Repository;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace DebitoCredito.Application.Services
{
    /// <summary>
    /// Implementacion metodos de negocio interface movimiento
    /// </summary>
    public class MovimientoService : IMovimientoService
    {
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly ICuentaService _cuentaService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="logger">registro log clase</param>
        /// <param name="mapper">interface para mapear datos</param>
        /// <param name="movimientoRepositoy">interface para acceso a datos</param>
        /// <param name="cuentaService">interface para acceder a informacion de cuenta</param>
        public MovimientoService(ILogger<MovimientoService> logger, IMapper mapper, IMovimientoRepository movimientoRepositoy,
             ICuentaService cuentaService)
        {
            _logger = logger;
            _mapper = mapper;
            _movimientoRepository = movimientoRepositoy;
            _cuentaService = cuentaService;
        }
        /// <summary>
        /// Permite realizar los movimientos de debito y credito de la cuenta
        /// </summary>
        /// <param name="movimiento">movimiento a realizar</param>
        /// <returns>Detalle del moviento</returns>
        public async Task<Response<List<DetalleMovimientoDto>>> AcreditarDebitar(MovimientosDto movimientoDto)
        {
            try
            {
                Response<CuentaDto> cuenta = await _cuentaService.ConsultarCuenta(movimientoDto.NumeroCuenta); 
                if(null != cuenta.Mensaje)
                {
                    if(cuenta.Mensaje.NumeroCuenta == 0) ///Se verifica que la cuenta exista
                    {
                        return Response<List<DetalleMovimientoDto>>.Warning(new List<DetalleMovimientoDto>() ,cuenta.Descripcion);
                    }
                }
                Movimientos movimiento = await _mapper.From(movimientoDto).AdaptToTypeAsync<Movimientos>();
                await _movimientoRepository.AcreditarDebitar(movimiento);
                Response<List<DetalleMovimientoDto>> movimientos = await ConsultarMovimientos(movimiento.Cuenta.NumeroCuenta);
                return movimientos;
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<List<DetalleMovimientoDto>>.Error(ex.Message);
            }
        }
        /// <summary>
        /// Permite realizar los movimientos realizados a la cuenta
        /// </summary>
        /// <param name="numeroCuenta">numero de cuenta</param>
        /// <returns>Detalle del moviento</returns>
        public async Task<Response<List<DetalleMovimientoDto>>> ConsultarMovimientos(int numeroCuenta)
        {
            try
            {
                List<Movimientos> movimientos = await _movimientoRepository.ConsultarMovimientos(numeroCuenta);
                if (movimientos != null && movimientos.Count > 0)
                {
                    List<DetalleMovimientoDto> detalleDto = (from mv in movimientos
                                                             orderby mv.FechaMovimiento descending
                                                             select new DetalleMovimientoDto
                                                             {
                                                                 NumeroCuenta = mv.Cuenta.NumeroCuenta,
                                                                 Tipo = mv.Cuenta.Tipo,
                                                                 SaldoInicial = mv.Cuenta.SaldoInicial,
                                                                 Movimiento = string.Format("{0} {1}", mv.TipoMovimiento, mv.Valor),
                                                                 FechaMovimiento = string.Format("{0:dd/MM/yyyy}", mv.FechaMovimiento),
                                                             }).ToList();

                    return Response<List<DetalleMovimientoDto>>.Ok(detalleDto, Constantes.ConstantesApi.MensajeTransaccionProcesada);
                }
                else
                {
                    return Response<List<DetalleMovimientoDto>>.Warning(new List<DetalleMovimientoDto>(), ConstantesApi.MensajeNoSeHanEncontradoMovimientos);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<List<DetalleMovimientoDto>>.Error(ConstantesApi.MensajeNoSeHanEncontradoMovimientos);
            }
        }
        /// <summary>
        /// Permite obtener un reporte de los movimientos realizados a la cuenta
        /// </summary>
        /// <param name="fechaInicio">fecha de inicio</param>
        /// <param name="fechaFin">fecha de fin</param>
        /// <param name="identificacion">identificacion</param>
        /// <returns>Detalle de movimiento</returns>
        public async  Task<Response<List<EstadoDeCuentaDto>>> ConsultarMovimientosPorFecha(DateTime fechaInicio, DateTime fechaFin, string identificacion)
        {
            try
            {
                List<Movimientos> movimientos = await _movimientoRepository.ConsultarMovimientosPorFecha(fechaInicio, fechaFin, identificacion);
                if(null != movimientos)
                {
                    List<EstadoDeCuentaDto> estadoDeCuenta = (from mv in movimientos
                                                              orderby mv.FechaMovimiento descending
                                                              select new EstadoDeCuentaDto
                                                              {
                                                                  Fecha = string.Format("{0:dd/MM/yyyy}",mv.FechaMovimiento),
                                                                  Cliente = mv.Cuenta.Cliente.Persona.Nombre,
                                                                  NumeroCuenta = mv.Cuenta.NumeroCuenta,
                                                                  Tipo = mv.Cuenta.Tipo,
                                                                  SaldoInicial = mv.Cuenta.SaldoInicial,
                                                                  Estado = mv.Cuenta.Estado,
                                                                  Movimiento = mv.Valor,
                                                                  SaldoDisponible = mv.Saldo,
                                                              }).ToList();

                    return Response<List<EstadoDeCuentaDto>>.Ok(estadoDeCuenta, ConstantesApi.MensajeTransaccionProcesada);
                }
                else
                {
                    return Response<List<EstadoDeCuentaDto>>.Warning(new List<EstadoDeCuentaDto>(), ConstantesApi.MensajeEstadoDeCuentaNoDisponible);
                }
               
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<List<EstadoDeCuentaDto>>.Error(ConstantesApi.MensajeEstadoDeCuentaError);
            }
        }
        /// <summary>
        /// Permite eliminar los movimientos de una cuenta
        /// </summary>
        /// <param name="cuentaId">identificacion de la cuenta</param>
        /// <returns>1 si fueron eliminados los movimientos o 0 si ocurrio un error al eliminar</returns>;
        public async Task<Response<int>> EliminarMovimientos(int cuentaId)
        {
            try
            {

                List<Movimientos> movimientos = await _movimientoRepository.ConsultarMovimientos(cuentaId);
                if(null != movimientos)
                {
                    if(movimientos.Count() == 0)
                    {
                        return Response<int>.Warning(0, ConstantesApi.MensajeCuentSinMovimientos);
                    }
                    else
                    {
                       int esFinTransaccion =  await _movimientoRepository.EliminarMovimientos(movimientos.First().CuentaId);
                       if(esFinTransaccion > 0)
                       {
                            return Response<int>.Ok(esFinTransaccion, ConstantesApi.MensajeMovimientosEliminados);
                        }
                        else
                        {
                            return Response<int>.Warning(esFinTransaccion, ConstantesApi.MensajeMovimientosError);
                        }
                    }
                }
                else
                {
                    return Response<int>.Warning(0, ConstantesApi.MensajeCuentaNoEncontrada);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<int>.Error(ConstantesApi.MensajeMovimientosError);
            }
        }
    }
}

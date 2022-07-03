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
    /// Implementacion metodos de negocio interface cuenta
    /// </summary>
    public class CuentaService : ICuentaService
    {
        private readonly ICuentaRepository _cuentaRepository;
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="logger">registro log clase</param>
        /// <param name="mapper">Interface para mapear datos</param>
        /// <param name="cuentaRepository">Interface para acceso a datos</param>
        /// <param name="clienteService">Interface para acceso a informacion de cliente</param>
        public CuentaService(ILogger<CuentaService> logger, IMapper mapper, ICuentaRepository cuentaRepository, IClienteService clienteService)
        {
            _logger = logger;
            _mapper = mapper;
            _cuentaRepository = cuentaRepository;
            _clienteService = clienteService;
        }
        /// <summary>
        /// Permite realizar la creacion de una cuenta
        /// </summary>
        /// <param name="cuenta">cuenta a crear</param>
        /// <returns>CuentaDto</returns>
        public async Task<Response<CuentaDto>> CrearCuenta(CuentaDto cuentaDto)
        {
            try
            {
                Response<ClienteDto> cliente = await _clienteService.ConsultarCliente(cuentaDto.Identificacion);
                if(string.IsNullOrEmpty(cliente.Mensaje.Identificacion))  ///Se verifica que exista el usuario antes de crear la cuenta
                {
                    return Response<CuentaDto>.Warning(new CuentaDto(), ConstantesApi.MensajeClienteNoExiste);
                }
                Cuenta cuentaCliente = await _cuentaRepository.ConsultarCuenta(cuentaDto.NumeroCuenta);///Se verifica que la cuenta ingresada no exista
                if (null != cuentaCliente)
                {
                    return Response<CuentaDto>.Warning(new CuentaDto(), ConstantesApi.MensajeCuentaYaExiste);
                }

                Cuenta cuenta = await _mapper.From(cuentaDto).AdaptToTypeAsync<Cuenta>();
                cuenta.ClienteId = cliente.Mensaje.ClienteId;
                int esFinTransaccion = await _cuentaRepository.CrearCuenta(cuenta);
                if (esFinTransaccion == 0)
                {
                    return Response<CuentaDto>.Warning(new CuentaDto(), ConstantesApi.MensajeErrorCreacionCuenta);
                }
                Cuenta detalleCuenta = await _cuentaRepository.ConsultarCuenta(cuentaDto.NumeroCuenta);
                CuentaDto detalleCuentaDto = new CuentaDto
                {
                    Identificacion = detalleCuenta.Cliente.Persona.Identificacion,
                    NumeroCuenta = detalleCuenta.NumeroCuenta,
                    Tipo = detalleCuenta.Tipo,
                    MontoDiario = detalleCuenta.MontoDiario,
                    SaldoInicial = detalleCuenta.SaldoInicial
                };
                return Response<CuentaDto>.Ok(detalleCuentaDto, ConstantesApi.MensajeCuentaCreada);
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<CuentaDto>.Error(ConstantesApi.MensajeErrorCreacionCuenta);
            }
        }
        /// <summary>
        /// Permite consultar la informacion de la cuenta del cliente
        /// </summary>
        /// <param name="numeroCuenta">numero cuenta</param>
        /// <returns>CuentaDto</returns>
        public async Task<Response<CuentaDto>> ConsultarCuenta(int numeroCuenta)
        {
            Cuenta cuenta = await _cuentaRepository.ConsultarCuenta(numeroCuenta);
            if(null == cuenta)
            {
                return Response<CuentaDto>.Warning(new CuentaDto(), ConstantesApi.MensajeCuentaNoEncontrada);
            }
            CuentaDto cuentaDto = await _mapper.From(cuenta).AdaptToTypeAsync<CuentaDto>();
            return Response<CuentaDto>.Ok(cuentaDto, ConstantesApi.MensajeTransaccionProcesada);

        }
        /// <summary>
        /// Permite actualizar la informacion de la cuenta del cliente
        /// </summary>
        /// <param name="cuenta">cuenta a actualizar</param>
        /// <returns>CuentaDto</returns>
        public async Task<Response<CuentaDto>> ActualizarCuenta(CuentaDto cuentaDto)
        {
            try
            {
                Response<CuentaDto> cuentaVerificar = await ConsultarCuenta(cuentaDto.NumeroCuenta);
                if (cuentaVerificar.Mensaje.NumeroCuenta == 0)
                {
                    return Response<CuentaDto>.Warning(new CuentaDto(), cuentaVerificar.Descripcion);
                }
                Cuenta cuenta = await _mapper.From(cuentaDto).AdaptToTypeAsync<Cuenta>();
                int esFinTransaccion = await _cuentaRepository.ActualizarCuenta(cuenta);
                if (esFinTransaccion > 0)
                {
                    Response<CuentaDto> cuentaResponse = await ConsultarCuenta(cuentaDto.NumeroCuenta);
                    return Response<CuentaDto>.Ok(cuentaResponse.Mensaje, ConstantesApi.MensajeCuentaActualizada);
                }
                else
                {
                    return Response<CuentaDto>.Warning(new CuentaDto(), ConstantesApi.MensajeCuentaErrorActualizar);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<CuentaDto>.Error(ConstantesApi.MensajeCuentaErrorActualizar);
            }
        }
        /// <summary>
        /// Permite eliminar la cuenta del cliente
        /// </summary>
        /// <param name="numeroCuenta">cuenta a eliminar</param>
        /// <returns>CuentaDto></returns>
        public async Task<Response<CuentaDto>> EliminarCuenta(int numeroCuenta)
        {
            try
            {
                Response<CuentaDto> cuenta = await ConsultarCuenta(numeroCuenta);
                if(null != cuenta.Mensaje)
                {
                    if(cuenta.Mensaje.NumeroCuenta  == 0)
                    {
                        return Response<CuentaDto>.Warning(new CuentaDto(), cuenta.Descripcion);
                    }
                }
                int totalMovimientoCuenta = await _cuentaRepository.ConsultarMovimientoCuenta(numeroCuenta);
                if(totalMovimientoCuenta > 0)
                {
                    return Response<CuentaDto>.Warning(new CuentaDto(), ConstantesApi.MensajeMovimientosCuenta);
                }
                int esFinTransaccion = await _cuentaRepository.EliminarCuenta(numeroCuenta);
                return Response<CuentaDto>.Ok(new CuentaDto(), ConstantesApi.MensajeCuentaEliminadaCorrectamente);
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<CuentaDto>.Error(ConstantesApi.MensajeErrorEliminarCuenta);
            }
        }

    }
}

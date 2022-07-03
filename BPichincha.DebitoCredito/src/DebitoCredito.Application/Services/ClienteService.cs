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
    /// Implementacion metodos de negocio interface cliente
    /// </summary>
    public class ClienteService: IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="logger">registro log clase</param>
        /// <param name="mapper">interface para mapear datos</param>
        /// <param name="clienteRepository">interface para acceso a datos</param>
        public ClienteService(ILogger<ClienteService> logger, IMapper mapper, IClienteRepository clienteRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _clienteRepository = clienteRepository;
           
        }
        /// <summary>
        /// Permite consultar la informacion de un cliente por identificacion
        /// </summary>
        /// <param name="identificacion">identificacion cliente</param>
        /// <returns>ClienteDto</returns>
        public async Task<Response<ClienteDto>> ConsultarCliente(string identificacion)
        {
            try
            {
                Cliente cliente =  await _clienteRepository.ConsultarCliente(identificacion);
                if(null == cliente)
                {
                    return Response<ClienteDto>.Warning(new ClienteDto(), ConstantesApi.MensajeClienteNoEncontrado);
                }
                ClienteDto clienteDto = await _mapper.From(cliente).AdaptToTypeAsync<ClienteDto>();
                return Response<ClienteDto>.Ok(clienteDto, ConstantesApi.MensajeTransaccionProcesada);
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<ClienteDto>.Error(ConstantesApi.MensajeErrorConsultaCliente);
            }
        }
        /// <summary>
        /// Permite realizar la creacion de clientes
        /// </summary>
        /// <param name="cliente">cliente a crear</param>
        /// <returns>ClienteDto</returns>
        public async Task<Response<ClienteDto>> CrearCliente(ClienteDto clienteDto)
        {
            try
            {
                Response<ClienteDto> clienteConsulta = await ConsultarCliente(clienteDto.Identificacion);
                if(string.IsNullOrEmpty(clienteConsulta.Mensaje.Identificacion))
                {
                    Cliente cliente = await _mapper.From(clienteDto).AdaptToTypeAsync<Cliente>();
                    int esFinTransaccion = await _clienteRepository.CrearCliente(cliente);
                    if (esFinTransaccion == 0)
                    {
                        return Response<ClienteDto>.Warning(new ClienteDto(), ConstantesApi.MensajeErrorCreacionCliente);
                    }
                    Response<ClienteDto> clienteResponse = await ConsultarCliente(clienteDto.Identificacion);
                    return Response<ClienteDto>.Ok(clienteResponse.Mensaje, ConstantesApi.MensajeClienteCreado);
                }
                else
                {
                    return Response<ClienteDto>.Warning(new ClienteDto(), ConstantesApi.MensajeClienteYaRegistrado);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<ClienteDto>.Error(ConstantesApi.MensajeErrorCreacionCliente);
            }
        }
        /// <summary>
        /// Permite actualizar la informacion del cliente
        /// </summary>
        /// <param name="clienteDto"></param>
        /// <returns>ClienteDto</returns>
        public async Task<Response<ClienteDto>> ActualizarCliente(ClienteDto clienteDto)
        {
            try
            {
                Response<ClienteDto> clienteConsulta = await ConsultarCliente(clienteDto.Identificacion);
                if(!string.IsNullOrEmpty(clienteConsulta.Mensaje.Identificacion))
                {
                    Cliente cliente = await _mapper.From(clienteDto).AdaptToTypeAsync<Cliente>();
                    int esFinTransaccion = await _clienteRepository.ActualizarCliente(cliente);
                    if (esFinTransaccion == 0)
                    {
                        return Response<ClienteDto>.Warning(new ClienteDto(), ConstantesApi.MensajeClienteErrorActualizacion);
                    }
                    Response<ClienteDto> clienteActualizado = await ConsultarCliente(clienteDto.Identificacion);
                    return Response<ClienteDto>.Ok(clienteActualizado.Mensaje, ConstantesApi.MensajeClienteActualizado);
                }
                else
                {
                    return Response<ClienteDto>.Warning(new ClienteDto(), ConstantesApi.MensajeClienteNoEncontrado);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return  Response<ClienteDto>.Warning(new ClienteDto(), ConstantesApi.MensajeClienteErrorActualizacion);
            }
        }
        /// <summary>
        /// Permite eliminar un cliente
        /// </summary>
        /// <param name="identificacion">identificacion del cliente a eliminar</param>
        /// <returns>ClienteDto</returns>
        public async Task<Response<ClienteDto>> EliminarCliente(string identificacion)
        {
            try
            {
                Response<ClienteDto> clienteConsulta = await ConsultarCliente(identificacion);
                if (string.IsNullOrEmpty(clienteConsulta.Mensaje.Identificacion))
                {
                    return Response<ClienteDto>.Warning(new ClienteDto(), clienteConsulta.Descripcion);
                }
                int totalCuentas = await _clienteRepository.ConsultarTotalCuentasCliente(identificacion);
                if(totalCuentas > 0)
                {
                    return Response<ClienteDto>.Warning(new ClienteDto(), ConstantesApi.MensajeCuentasAsociadasCliente);
                }
                int esFinTransaccion = await _clienteRepository.EliminarCliente(identificacion);
                return Response<ClienteDto>.Ok(new ClienteDto(), ConstantesApi.MensajeClienteEliminadoCorrectamente);
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<ClienteDto>.Error(ConstantesApi.MensajeErrorEliminacionCliente);
            }
        }
    }
}

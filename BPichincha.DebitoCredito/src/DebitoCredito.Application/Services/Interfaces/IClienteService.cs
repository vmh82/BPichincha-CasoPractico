using DebitoCredito.Application.Dto;
using DebitoCredito.Application.Util;

namespace DebitoCredito.Application.Services.Interfaces
{
    /// <summary>
    /// Interface con definicion de metodos de negocio cliente
    /// </summary>
    public interface IClienteService
    {
        /// <summary>
        /// Permite realizar la creacion de clientes
        /// </summary>
        /// <param name="cliente">cliente a crear</param>
        /// <returns>ClienteDto</returns>
        Task<Response<ClienteDto>> CrearCliente(ClienteDto cliente);
        /// <summary>
        /// Permite consultar la informacion de un cliente por identificacion
        /// </summary>
        /// <param name="identificacion">identificacion cliente</param>
        /// <returns>ClienteDto</returns>
        Task<Response<ClienteDto>> ConsultarCliente(string identificacion);
        /// <summary>
        /// Permite actualizar la informacion del cliente
        /// </summary>
        /// <param name="clienteDto"></param>
        /// <returns>ClienteDto</returns>
        Task<Response<ClienteDto>> ActualizarCliente(ClienteDto clienteDto);
        /// <summary>
        /// Permite eliminar un cliente
        /// </summary>
        /// <param name="identificacion">identificacion del cliente a eliminar</param>
        /// <returns>ClienteDto</returns>
        Task<Response<ClienteDto>> EliminarCliente(string identificacion);
    }
}

using DebitoCredito.Application.Dto;
using DebitoCredito.Application.Util;


namespace DebitoCredito.Application.Services.Interfaces
{
    /// <summary>
    /// Interface con definicion de metodos de negocio cuenta
    /// </summary>
    public interface ICuentaService
    {
        /// <summary>
        /// Permite realizar la creacion de una cuenta
        /// </summary>
        /// <param name="cuenta">cuenta a crear</param>
        /// <returns>CuentaDto</returns>
        Task<Response<CuentaDto>> CrearCuenta(CuentaDto cuenta);
        /// <summary>
        /// Permite consultar la informacion de la cuenta del cliente
        /// </summary>
        /// <param name="numeroCuenta">numero cuenta</param>
        /// <returns>CuentaDto</returns>
        Task<Response<CuentaDto>> ConsultarCuenta(int numeroCuenta);
        /// <summary>
        /// Permite actualizar la informacion de la cuenta del cliente
        /// </summary>
        /// <param name="cuenta">cuenta a actualizar</param>
        /// <returns>CuentaDto</returns>
        Task<Response<CuentaDto>> ActualizarCuenta(CuentaDto cuenta);
        /// <summary>
        /// Permite eliminar la cuenta del cliente
        /// </summary>
        /// <param name="numeroCuenta">cuenta a eliminar</param>
        /// <returns>CuentaDto></returns>
        Task<Response<CuentaDto>> EliminarCuenta(int numeroCuenta);

    }
}

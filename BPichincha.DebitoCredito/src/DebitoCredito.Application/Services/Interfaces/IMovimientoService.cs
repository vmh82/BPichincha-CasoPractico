using DebitoCredito.Application.Dto;
using DebitoCredito.Application.Util;
namespace DebitoCredito.Application.Services.Interfaces
{
    /// <summary>
    /// Interface con definicion de metodos de negocio cuenta
    /// </summary>
    public interface IMovimientoService
    {
        /// <summary>
        /// Permite realizar los movimientos de debito y credito de la cuenta
        /// </summary>
        /// <param name="movimiento">movimiento a realizar</param>
        /// <returns>Detalle del moviento</returns>
        Task<Response<List<DetalleMovimientoDto>>> AcreditarDebitar(MovimientosDto movimiento);
        /// <summary>
        /// Permite realizar los movimientos realizados a la cuenta
        /// </summary>
        /// <param name="numeroCuenta">numero de cuenta</param>
        /// <returns>Detalle del moviento</returns>
        Task<Response<List<DetalleMovimientoDto>>> ConsultarMovimientos(int numeroCuenta);
        /// <summary>
        /// Permite obtener un reporte de los movimientos realizados a la cuenta
        /// </summary>
        /// <param name="fechaInicio">fecha de inicio</param>
        /// <param name="fechaFin">fecha de fin</param>
        /// <param name="identificacion">identificacion</param>
        /// <returns>Detalle de movimiento</returns>
        Task<Response<List<EstadoDeCuentaDto>>> ConsultarMovimientosPorFecha(DateTime fechaInicio, DateTime fechaFin, string identificacion);
        /// <summary>
        /// Permite eliminar los movimientos de una cuenta
        /// </summary>
        /// <param name="cuentaId">identificacion de la cuenta</param>
        /// <returns>1 si fueron eliminados los movimientos o 0 si ocurrio un error al eliminar</returns>
        Task<Response<int>> EliminarMovimientos(int cuentaId);
    }
}

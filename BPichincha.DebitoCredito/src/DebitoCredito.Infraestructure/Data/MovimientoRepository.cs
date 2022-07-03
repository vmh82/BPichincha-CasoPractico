

using DebitoCredito.Domain.Entidades;
using DebitoCredito.Domain.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Globalization;

namespace DebitoCredito.Infraestructure.Data
{
    /// <summary>
    /// Implementacion metodos de acceso a datos movimiento
    /// </summary>
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly DebitoCreditoDBContext _context;
        public MovimientoRepository(DebitoCreditoDBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Permite realizar los movimientos de debito y credito de la cuenta
        /// </summary>
        /// <param name="movimiento">movimiento a realizar</param>
        /// <returns>Detalle del moviento</returns>
        public async Task<int> AcreditarDebitar(Movimientos movimientos)
        {
            SqlParameter[] parametros = new[] {
            new SqlParameter("@i_numero_cuenta", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = movimientos.Cuenta.NumeroCuenta },
            new SqlParameter("@i_valor", SqlDbType.Decimal) { Direction = ParameterDirection.Input, Value = movimientos.Valor }
            };
            return await _context.Database.ExecuteSqlRawAsync("[dbo].[spi_creditodebito] @i_numero_cuenta, @i_valor", parametros);

        }
        /// <summary>
        /// Permite realizar los movimientos realizados a la cuenta
        /// </summary>
        /// <param name="numeroCuenta">numero de cuenta</param>
        /// <returns>Detalle del moviento</returns>
        public async Task<List<Movimientos>> ConsultarMovimientos(int numeroCuenta)
        {
            var query = await (from m in _context.Movimientos
                               join c in _context.Cuenta on m.CuentaId equals c.CuentaId
                               where c.NumeroCuenta.Equals(numeroCuenta)
                               select m).OrderByDescending(f => f.FechaMovimiento).Include(c => c.Cuenta).ToListAsync();
            return query;
        }

        /// <summary>
        /// Permite obtener un reporte de los movimientos realizados a la cuenta
        /// </summary>
        /// <param name="fechaInicio">fecha de inicio</param>
        /// <param name="fechaFin">fecha de fin</param>
        /// <param name="identificacion">identificacion</param>
        /// <returns>Detalle de movimiento</returns>
        public async Task<List<Movimientos>> ConsultarMovimientosPorFecha(DateTime fechaInicio, DateTime fechaFin, string identificacion)
        {
            var query = await (from m in _context.Movimientos
                               join c in _context.Cuenta on m.CuentaId equals c.CuentaId
                               join cl in _context.Cliente on c.ClienteId equals cl.ClienteId
                               where cl.Persona.Identificacion.Equals(identificacion) &&
                               m.FechaMovimiento.Date >= fechaInicio.Date && m.FechaMovimiento.Date <= fechaFin.Date
                               select m
                               ).OrderByDescending(f => f.FechaMovimiento).Include(c => c.Cuenta.Cliente.Persona).ToListAsync();
            return query;
        }
        /// <summary>
        /// Permite eliminar los movimientos de una cuenta
        /// </summary>
        /// <param name="cuentaId">identificacion de la cuenta</param>
        /// <returns>1 si fueron eliminados los movimientos o 0 si ocurrio un error al eliminar</returns>
        public async Task<int> EliminarMovimientos(int cuentaId)
        {
            _context.Movimientos.RemoveRange(_context.Movimientos.Where(m=>m.CuentaId == cuentaId));
            return await _context.SaveChangesAsync();
        }
    }
}

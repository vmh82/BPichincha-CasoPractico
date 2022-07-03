using DebitoCredito.Domain.Entidades;
using DebitoCredito.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace DebitoCredito.Infraestructure.Data
{
    /// <summary>
    /// Implementacion de metodos de acceso a datos cuenta
    /// </summary>
    public class CuentaRepository : ICuentaRepository
    {
        private readonly DebitoCreditoDBContext _context;
        public CuentaRepository(DebitoCreditoDBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Permite consultar la informacion de la cuenta del cliente
        /// </summary>
        /// <param name="numeroCuenta">numero cuenta</param>
        /// <returns>CuentaDto</returns>
        public async Task<Cuenta> ConsultarCuenta(int numeroCuenta)
        {
            var query = await (from c in _context.Cuenta
                               join cl in _context.Cliente on c.ClienteId equals cl.ClienteId
                               where c.NumeroCuenta == numeroCuenta
                               select c).Include(res => res.Cliente).Include(res => res.Cliente.Persona).FirstOrDefaultAsync();

            return query;
        }

        /// <summary>
        /// Permite realizar la creacion de una cuenta
        /// </summary>
        /// <param name="cuenta">cuenta a crear</param>
        /// <returns>CuentaDto</returns>
        public async Task<int> CrearCuenta(Cuenta cuenta)
        {
            cuenta.Estado = true;
            _context.Set<Cuenta>().Add(cuenta);
            return await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Permite actualizar la informacion de la cuenta del cliente
        /// </summary>
        /// <param name="cuenta">cuenta a actualizar</param>
        /// <returns>CuentaDto</returns>
        public async Task<int> ActualizarCuenta(Cuenta cuenta)
        {
            Cuenta cuentaConsulta = await ConsultarCuenta(cuenta.NumeroCuenta);
            cuentaConsulta.Tipo = cuenta.Tipo;
            cuentaConsulta.SaldoInicial = cuenta.SaldoInicial;
            cuentaConsulta.MontoDiario = cuenta.MontoDiario;
            _context.Update(cuentaConsulta);
            return await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Permite eliminar la cuenta del cliente
        /// </summary>
        /// <param name="numeroCuenta">cuenta a eliminar</param>
        /// <returns>CuentaDto></returns>
        public async Task<int> EliminarCuenta(int numeroCuenta)
        {
            Cuenta cuenta = await ConsultarCuenta(numeroCuenta);
            _context.Cuenta.Attach(cuenta);
            _context.Cuenta.RemoveRange(cuenta);
            return await _context.SaveChangesAsync();

        }
        public async Task<int> ConsultarMovimientoCuenta(int numeroCuenta)
        {
            var query = await (from mv in _context.Movimientos
                               join c in _context.Cuenta on mv.CuentaId equals c.CuentaId
                               where c.NumeroCuenta == numeroCuenta
                               select mv).CountAsync();
            return query;
        }
    }
}

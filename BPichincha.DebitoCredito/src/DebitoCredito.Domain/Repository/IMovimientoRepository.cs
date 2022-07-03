using DebitoCredito.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebitoCredito.Domain.Repository
{
    /// <summary>
    /// Interface con definicion de metodos para acceso a datos
    /// </summary>
    public interface IMovimientoRepository
    {
       Task<int> AcreditarDebitar(Movimientos movimientos);
       Task<List<Movimientos>> ConsultarMovimientos(int numeroCuenta);
       Task<List<Movimientos>> ConsultarMovimientosPorFecha(DateTime fechaInicio, DateTime fechaFin, string identificacion);

        Task<int> EliminarMovimientos(int cuentaId);
    }
}

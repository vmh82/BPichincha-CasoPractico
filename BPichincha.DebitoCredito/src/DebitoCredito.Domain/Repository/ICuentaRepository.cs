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
    public interface ICuentaRepository
    {
        Task<int> CrearCuenta(Cuenta cuenta);
        Task<Cuenta> ConsultarCuenta(int numeroCuenta);
        Task<int> ActualizarCuenta(Cuenta cuenta);
        Task<int> EliminarCuenta(int numeroCuenta);
        Task<int> ConsultarMovimientoCuenta(int numeroCuenta);


    }
}

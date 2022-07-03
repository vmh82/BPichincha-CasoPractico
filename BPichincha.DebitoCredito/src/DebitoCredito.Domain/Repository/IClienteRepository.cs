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
    public interface IClienteRepository
    {
        Task<int> CrearCliente(Cliente cliente);
        Task<Cliente> ConsultarCliente(string identificacion);

        Task<int> EliminarCliente(string identificacion);

        Task<int> ActualizarCliente(Cliente cliente);

        Task<int> ConsultarTotalCuentasCliente(string identificacion);
    }
}

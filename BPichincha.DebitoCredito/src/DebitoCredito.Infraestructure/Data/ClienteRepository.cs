using DebitoCredito.Domain.Entidades;
using DebitoCredito.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace DebitoCredito.Infraestructure.Data
{
    /// <summary>
    /// Implementacion de metodos de acceso a datos cliente
    /// </summary>
    public class ClienteRepository : IClienteRepository
    {
        private readonly DebitoCreditoDBContext _context;

        public ClienteRepository(DebitoCreditoDBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Permite realizar la creacion de clientes
        /// </summary>
        /// <param name="cliente">cliente a crear</param>
        /// <returns>1 si fue creado, 0 si ocurrio un error al crear</returns>
        public async Task<int> CrearCliente(Cliente cliente)
        {
            cliente.Estado = true;
            _context.Cliente.Add(cliente);
            return await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Permite consultar la informacion de un cliente por identificacion
        /// </summary>
        /// <param name="identificacion">identificacion cliente</param>
        /// <returns>ClienteDto</returns>
        public async Task<Cliente> ConsultarCliente(string identificacion)
        {
            var query = await (from c in _context.Cliente
                         join p in _context.Persona on c.PersonaId equals p.PersonaId
                         where p.Identificacion == identificacion
                         select c).Include(res => res.Persona).FirstOrDefaultAsync();
            return query;
        }
        /// <summary>
        /// Permite eliminar un cliente
        /// </summary>
        /// <param name="identificacion">identificacion del cliente a eliminar</param>
        /// <returns>ClienteDto</returns>
        public async Task<int> EliminarCliente(string identificacion)
        {
            Cliente clienteConsulta = await ConsultarCliente(identificacion);

            _context.Cliente.Attach(clienteConsulta);
            _context.Cliente.RemoveRange(clienteConsulta);
            await _context.SaveChangesAsync();

            _context.Persona.Attach(clienteConsulta.Persona);
            _context.Persona.RemoveRange(clienteConsulta.Persona);
            return await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Permite actualizar la informacion del cliente
        /// </summary>
        /// <param name="clienteDto"></param>
        /// <returns>ClienteDto</returns>
        public async Task<int> ActualizarCliente(Cliente cliente)
        {
            Cliente clienteConsulta = await ConsultarCliente(cliente.Persona.Identificacion);
            clienteConsulta.Estado = cliente.Estado;
            clienteConsulta.Contrasena = cliente.Contrasena;
            clienteConsulta.Persona.Identificacion = cliente.Persona.Identificacion;
            clienteConsulta.Persona.Nombre = cliente.Persona.Nombre;
            clienteConsulta.Persona.Genero = cliente.Persona.Genero;
            clienteConsulta.Persona.Edad = cliente.Persona.Edad;
            clienteConsulta.Persona.Direccion = cliente.Persona.Direccion;
            clienteConsulta.Persona.Telefono = cliente.Persona.Telefono;
            _context.Update(clienteConsulta);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> ConsultarTotalCuentasCliente(string  identificacion)
        {

            int numeroCuentas = await (from ct in _context.Cuenta
                                       join c in _context.Cliente on ct.ClienteId equals c.ClienteId
                                       join p in _context.Persona on c.PersonaId equals p.PersonaId
                                       where p.Identificacion == identificacion
                                       select c).Include(res => res.Persona).CountAsync();
            return numeroCuentas;
        }

    }
}

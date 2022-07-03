using DebitoCredito.Application.Dto;
using DebitoCredito.Domain.Entidades;
using Mapster;
namespace DebitoCredito.Application.Mapper
{
    /// <summary>
    /// Clase para realizar el mapeo de datos entre objetos dto y entidades
    /// </summary>
    public class MapperConfig
    {
        /// <summary>
        /// Configuracion mapeador
        /// </summary>
        /// <returns></returns>
        public static TypeAdapterConfig ConfigurarMapper()
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<ClienteDto, Cliente>()
                .Map(dst => dst.Persona.Identificacion, org => org.Identificacion)
                .Map(dst => dst.Persona.Nombre, org => org.Nombre)
                .Map(dst => dst.Persona.Direccion, org => org.Direccion)
                .Map(dst => dst.Persona.Edad, org => org.Edad)
                .Map(dst => dst.Persona.Genero, org => org.Genero)
                .Map(dst => dst.Persona.Telefono, org => org.Telefono)
                .Map(dst => dst.Contrasena, org => org.Contrasena)
                 .Map(dst => dst.ClienteId, org => org.ClienteId)
                .IgnoreNonMapped(true);

            config.NewConfig<Cliente, ClienteDto>()
               .Map(dst => dst.Identificacion, org => org.Persona.Identificacion)
               .Map(dst => dst.Genero, org => org.Persona.Genero)
               .Map(dst => dst.Edad, org => org.Persona.Edad)
               .Map(dst => dst.Direccion, org => org.Persona.Direccion)
               .Map(dst => dst.Telefono, org => org.Persona.Telefono)
               .Map(dst => dst.Telefono, org => org.Persona.Telefono)
               .Map(dst => dst.Nombre, org => org.Persona.Nombre)
               .Map(dst => dst.ClienteId, org => org.ClienteId)
               .IgnoreNonMapped(true);


            config.NewConfig<CuentaDto, Cuenta>()
               .Map(dst => dst.ClienteId, org => org.ClienteId)
               .Map(dst => dst.SaldoInicial, org => org.SaldoInicial)
               .Map(dst => dst.Tipo, org => org.Tipo)
               .Map(dst => dst.NumeroCuenta, org => org.NumeroCuenta)
               .Map(dst => dst.MontoDiario, org => org.MontoDiario)
               .IgnoreNonMapped(true);


            config.NewConfig<Cuenta, CuentaDto>()
               .Map(dst => dst.ClienteId, org => org.ClienteId)
               .Map(dst => dst.SaldoInicial, org => org.SaldoInicial)
               .Map(dst => dst.MontoDiario, org => org.MontoDiario)
               .Map(dst => dst.Tipo, org => org.Tipo)
               .Map(dst => dst.NumeroCuenta, org => org.NumeroCuenta)
               .Map(dst => dst.Identificacion, org => org.Cliente.Persona.Identificacion)
               .IgnoreNonMapped(true);

            config.NewConfig<MovimientosDto, Movimientos>()
             .Map(dst => dst.Cuenta.NumeroCuenta, org => org.NumeroCuenta)
             .Map(dst => dst.Valor, org => org.Monto)
             .IgnoreNonMapped(true);


            return config;
        }
    }
}

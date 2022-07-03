namespace DebitoCredito.Application.Constantes
{
    /// <summary>
    /// Clase con constantes API
    /// </summary>
    public static class ConstantesApi
    {
        #region Mensajes
        public const string MensajeTransaccionProcesada = "Transaccion procesada con exito";
        public const string MensajeTransaccionError = "Ocurrio un error al procesar la transaccion";
        public const string MensajeClienteCreado = "Cliente creado correctamente";
        public const string MensajeClienteYaRegistrado = "Cliente ya se encuentra registrado";
        public const string MensajeErrorCreacionCliente = "Ocurrio un error al crear el cliente";
        public const string MensajeClienteActualizado = "Cliente Actualizado correctamente";
        public const string MensajeClienteErrorActualizacion = "Ocurrio un error al Actualizar el cliente";
        public const string MensajeClienteNoEncontrado = "Cliente no encontrado";
        public const string MensajeErrorConsultaCliente = "Ocurrio un error al consultar el cliente";
        public const string MensajeClienteNoExiste = "Cliente no encontrado, por favor verifica que el cliente haya sido creado para proceder con la apertura de la cuenta ";
        public const string MensajeCuentaYaExiste = "El numero de cuenta ya se encuentra asignado, por favor intenta con uno nuevo";
        public const string MensajeErrorCreacionCuenta = "Ocurrio un error al crear la cuenta";
        public const string MensajeCuentaCreada = "Cuenta creada correctamente";
        public const string MensajeCuentaNoEncontrada = "Cuenta No Encontrada";
        public const string MensajeCuentaActualizada = "Cuenta Actualizada correctamente";
        public const string MensajeCuentaErrorActualizar = "Ocurrio un error al actualizar la cuenta";
        public const string MensajeNoSeHanEncontradoMovimientos = "No se han encontrado movimientos en tu cuenta";
        public const string MensajeEstadoDeCuentaNoDisponible = "Estado de cuenta no disponible";
        public const string MensajeEstadoDeCuentaError = "Ocurrio un error al procesar el estado de cuenta";
        public const string MensajeClienteEliminadoCorrectamente = "Cliente eliminado correctamente";
        public const string MensajeCuentasAsociadasCliente = "Existen cuentas asociandas al cliente, debe eliminarlas para continuar";
        public const string MensajeErrorEliminacionCliente= "Ocurrio un error al eliminar el cliente";
        public const string MensajeCuentaEliminadaCorrectamente = "Cuenta eliminada correctamente";
        public const string MensajeMovimientosCuenta = "La cuenta posee movimientos asociados, debe eliminarlos para continuar";
        public const string MensajeErrorEliminarCuenta = "Ocurrio un error al eliminar la cuenta";
        public const string MensajeCuentSinMovimientos = "La cuenta no posee movimientos";
        public const string MensajeMovimientosEliminados = "Movimientos eliminados correctamente";
        public const string MensajeMovimientosError= "Ocurrio un error al eliminar los movimientos";
        #endregion
        #region ProcedimientoAlmacenados
        public const string SpiCreditoDebio = "spi_creditodebito";
        #endregion
    }
}

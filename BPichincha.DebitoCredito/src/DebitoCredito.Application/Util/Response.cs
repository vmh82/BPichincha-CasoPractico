using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DebitoCredito.Application.Util
{
    // <summary>
    ///  Clase para agregar informacion adicional a la respuesta enviada al cliente
    /// </summary>
    /// <typeparam name="TResponse">Tipo de dato a retornar</typeparam>
    public class Response<TResponse>
    {
        public HttpStatusCode Codigo { get; }
        public bool EsError { get; }
        public TResponse Mensaje { get; set; }
        public object Descripcion { get; set; }

        public Response(TResponse response, string descripcion)
        {
            Mensaje = response;
            EsError = false;
            Descripcion = descripcion;
            Codigo = HttpStatusCode.OK;
        }
        protected Response(TResponse response, object mensajeControlado)
        {
            Mensaje = response;
            Descripcion = mensajeControlado;
            EsError = false;
            Codigo = HttpStatusCode.OK;
        }

        protected Response(string mensajeError)
        {
            Descripcion = mensajeError;
            EsError = true;
            Codigo = HttpStatusCode.OK;
        }

        public static Response<TResponse> Error(string error)
        {
            return new Response<TResponse>(error);
        }
        public static Response<TResponse> Ok(TResponse response, string descripcion)
        {
            return new Response<TResponse>(response, descripcion);
        }

        public static Response<TResponse> Warning(TResponse response, object mensajeControlado)
        {
            return new Response<TResponse>(response, mensajeControlado);
        }
    }
}

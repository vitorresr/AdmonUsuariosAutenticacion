using System;
using System.Collections.Generic;
using System.Text;

namespace AdmonUsuarios.Entidades.Transversales
{
    public class RespuestaServicio
    {
        public int Codigo { get; set; }

        public string Mensaje { get; set; }

        public Object Data { get; set; }

        public RespuestaServicio ObtenerRespuestaCorrecta(Object data, string mensaje)
        {
            RespuestaServicio respuesta = new RespuestaServicio();
            respuesta.Codigo = 0;
            respuesta.Mensaje = mensaje;
            respuesta.Data = data;

            return respuesta;
        }

        public RespuestaServicio ObtenerRespuestaIncorrecta(int codigoError, string mensaje)
        {
            RespuestaServicio respuesta = new RespuestaServicio();
            respuesta.Codigo = codigoError;
            respuesta.Mensaje = mensaje;

            return respuesta;
        }
    }
}

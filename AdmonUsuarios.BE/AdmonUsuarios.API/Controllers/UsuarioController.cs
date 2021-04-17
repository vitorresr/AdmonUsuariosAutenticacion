using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdmonUsuarios.BLL;
using AdmonUsuarios.DAL.Clases;
using AdmonUsuarios.Entidades.Context;
using AdmonUsuarios.Entidades.Modelo;
using AdmonUsuarios.Entidades.Transversales;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AdmonUsuarios.API.Controllers
{
    [Route("api/Usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UserDBContext _context;

        private UsuarioBLL _negocioBLL;

        public UsuarioController(UserDBContext context)
        {
            this._context = context;
            this._negocioBLL = new UsuarioBLL(new UsuarioDAL(this._context));
        }

        [HttpGet]
        public ActionResult<RespuestaServicio> ObtenerUsuarios()
        {
            RespuestaServicio response = new RespuestaServicio();

            try
            {
                var users = this._negocioBLL.ObtenerUsuarios();
                response = response.ObtenerRespuestaCorrecta(users, string.Empty);
            }
            catch (Exception ex)
            {
                response = response.ObtenerRespuestaIncorrecta(ex.GetHashCode(), ex.Message);
            }

            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return Ok(JsonConvert.SerializeObject(response, jsonSerializerSettings));
        }

        [HttpPost]
        public ActionResult<RespuestaServicio> CrearUsuario(Usuario user)
        {
            RespuestaServicio response = new RespuestaServicio();
            try
            {
                response = this._negocioBLL.CrearUsuario(user);
            }
            catch (Exception ex)
            {
                response = response.ObtenerRespuestaIncorrecta(ex.GetHashCode(), ex.Message);
            }

            return Ok(response);
        }

        [HttpPut]
        public ActionResult<RespuestaServicio> ActualizarUsuario(Usuario user)
        {
            RespuestaServicio response = new RespuestaServicio();

            try
            {
                response = this._negocioBLL.ActualizarUsuario(user);
            }
            catch (Exception ex)
            {
                response = response.ObtenerRespuestaIncorrecta(ex.GetHashCode(), ex.Message);
            }

            return response;
        }

        [HttpDelete]
        public ActionResult<RespuestaServicio> EliminarUsuario(string login)
        {
            RespuestaServicio response = new RespuestaServicio();

            try
            {
                response = this._negocioBLL.EliminarUsuario(login);
            }
            catch (Exception ex)
            {
                response = response.ObtenerRespuestaIncorrecta(ex.GetHashCode(), ex.Message);
            }

            return response;
        }

        [HttpGet("ConsultaUsuario/{login}")]
        public ActionResult<RespuestaServicio> ConsultarUsuarioID(string login)
        {
            RespuestaServicio response = new RespuestaServicio();
            var user = this._negocioBLL.ConsultarUsuarioID(login);
            response = response.ObtenerRespuestaCorrecta(user, string.Empty);
            return response;
        }

        [HttpGet("ObtenerPermisos/{login}")]
        public ActionResult<RespuestaServicio> ObtenerPermisosUsuario(string login)
        {
            RespuestaServicio response = new RespuestaServicio();
            var user = this._negocioBLL.ObtenerPermisosUsuario(login);
            response = response.ObtenerRespuestaCorrecta(user, string.Empty);
            return response;
        }
    }
}

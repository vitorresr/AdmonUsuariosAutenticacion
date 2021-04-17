using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdmonUsuarios.DAL.Interfaces;
using AdmonUsuarios.Entidades.Context;
using AdmonUsuarios.Entidades.Modelo;
using AdmonUsuarios.Entidades.Transversales;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AdmonUsuarios.DAL.Clases
{
    public class UsuarioDAL : IUsuarioDAL
    {
        private readonly UserDBContext _context;

        public UsuarioDAL(UserDBContext context)
        {
            _context = context;
        }
        public RespuestaServicio ActualizarUsuario(string login, Usuario user)
        {
            RespuestaServicio response = new RespuestaServicio();

            _context.Entry(user).State = EntityState.Modified;

            if (!UsuarioExiste(user.Usuario_Login))
            {
                response = response.ObtenerRespuestaIncorrecta(404, "El usuario no existe");
            }
            else
            {
                _context.SaveChanges();
                response = response.ObtenerRespuestaCorrecta(null, "El usuario fue actualizado correctamente");
            }

            return response;
        }

        public Usuario ConsultarUsuarioID(string login)
        {
            var usuario = _context.Usuario.Find(login);
            return usuario;
        }

        public RespuestaServicio CrearUsuario(Usuario user)
        {
            RespuestaServicio response = new RespuestaServicio();

            if (UsuarioExiste(user.Usuario_Login))
            {
                response = response.ObtenerRespuestaIncorrecta(10, "El usuario ya existe");
            }
            else
            {
                _context.Usuario.Add(user);
                _context.SaveChanges();
                var userBD = _context.Usuario.Find(user.Usuario_Login);
                response = response.ObtenerRespuestaCorrecta(userBD, "El usuario fue creado correctamente");
            }

            return response;
        }

        public RespuestaServicio EliminarUsuario(string login)
        {
            RespuestaServicio response = new RespuestaServicio();
            
            if (!UsuarioExiste(login))
            {
                response = response.ObtenerRespuestaIncorrecta(404, "El usuario no existe");
            }
            else
            {
                Usuario user = new Usuario();
                user.Usuario_Login = login;

                _context.Entry(user).State = EntityState.Deleted;
                _context.SaveChanges();
                response = response.ObtenerRespuestaCorrecta(null, "El usuario fue eliminado correctamente");
            }

            return response;
        }

        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            var usuarios = _context.Usuario;
            return usuarios;
        }

        private bool UsuarioExiste(string login)
        {
            return _context.Usuario.Any(e => e.Usuario_Login.Equals(login));
        }

        public PermisosUsuario ObtenerPermisosUsuario(string login)
        {
            var param = new SqlParameter("@usuario_login", login);

            var permisos = _context.Permisos.FromSqlRaw("sp_consulta_permisos @usuario_login", param).ToList();

            return permisos.FirstOrDefault();

        }
    }
}

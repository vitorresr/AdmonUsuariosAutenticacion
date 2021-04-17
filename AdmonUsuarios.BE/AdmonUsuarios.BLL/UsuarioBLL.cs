using System;
using System.Collections.Generic;
using System.Text;
using AdmonUsuarios.DAL.Interfaces;
using AdmonUsuarios.Entidades.Modelo;
using AdmonUsuarios.Entidades.Transversales;

namespace AdmonUsuarios.BLL
{
    public class UsuarioBLL
    {
        private IUsuarioDAL _repositorioDAL;

        public UsuarioBLL(IUsuarioDAL repositorioDAL)
        {
            this._repositorioDAL = repositorioDAL;
        }

        public RespuestaServicio ActualizarUsuario(Usuario user)
        {
            return _repositorioDAL.ActualizarUsuario(user.Usuario_Login, user);
        }

        public Usuario ConsultarUsuarioID(string login)
        {
            return _repositorioDAL.ConsultarUsuarioID(login);
        }

        public RespuestaServicio CrearUsuario(Usuario user)
        {
            return _repositorioDAL.CrearUsuario(user);
        }

        public RespuestaServicio EliminarUsuario(string login)
        {
            return _repositorioDAL.EliminarUsuario(login);
        }

        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            return _repositorioDAL.ObtenerUsuarios();
        }

        public PermisosUsuario ObtenerPermisosUsuario(string login)
        {
            return _repositorioDAL.ObtenerPermisosUsuario(login);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using AdmonUsuarios.Entidades.Modelo;
using AdmonUsuarios.Entidades.Transversales;

namespace AdmonUsuarios.DAL.Interfaces
{
    public interface IUsuarioDAL
    {
        IEnumerable<Usuario> ObtenerUsuarios();

        Usuario ConsultarUsuarioID(string login);

        RespuestaServicio CrearUsuario(Usuario user);

        RespuestaServicio ActualizarUsuario(string login, Usuario user);

        RespuestaServicio EliminarUsuario(string login);

        PermisosUsuario ObtenerPermisosUsuario(string login);
    }
}

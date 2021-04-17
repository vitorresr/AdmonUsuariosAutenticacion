using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdmonUsuarios.Entidades.Modelo
{
    public class PermisosUsuario
    {
        [Key]
        public string Usuario_Login { get; set; }
        public int ListarUsuarios { get; set; }

        public int FiltrarNombre { get; set; }

        public int FiltrarRol { get; set; }

        public int EditarUsuario { get; set; }

        public int CrearUsuario { get; set; }

        public int EliminarUsuario { get; set; }
    }
}

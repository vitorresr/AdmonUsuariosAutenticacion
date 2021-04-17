using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmonUsuarios.FE.Models
{
    public class PermisosUsuario
    {
        public string Usuario_Login { get; set; }
        public int ListarUsuarios { get; set; }

        public int FiltrarNombre { get; set; }

        public int FiltrarRol { get; set; }

        public int EditarUsuario { get; set; }

        public int CrearUsuario { get; set; }

        public int EliminarUsuario { get; set; }
    }
}

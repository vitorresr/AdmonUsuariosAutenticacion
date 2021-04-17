using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmonUsuarios.FE.Models
{
    public class UsuarioModel
    {
        public string Usuario_Login { get; set; }
        public string Usuario_Nombre { get; set; }
        public string Usuario_Password { get; set; }
        public string Usuario_Direccion { get; set; }
        public string Usuario_Telefono { get; set; }
        public string Usuario_Email { get; set; }
        public int Rol_Id { get; set; }
    }
}

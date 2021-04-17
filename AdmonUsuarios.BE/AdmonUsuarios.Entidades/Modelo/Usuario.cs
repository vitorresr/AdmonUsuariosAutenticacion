using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdmonUsuarios.Entidades.Modelo
{
    public class Usuario
    {
        [Key]
        public string Usuario_Login { get; set; }
        public string Usuario_Nombre { get; set; }
        public string Usuario_Password { get; set; }
        public string Usuario_Direccion { get; set; }
        public string Usuario_Telefono { get; set; }
        public string Usuario_Email { get; set; }
        public int Rol_Id { get; set; }
    }
}

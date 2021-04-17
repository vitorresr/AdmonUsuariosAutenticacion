using System;
using System.Collections.Generic;
using System.Text;
using AdmonUsuarios.Entidades.Modelo;
using Microsoft.EntityFrameworkCore;

namespace AdmonUsuarios.Entidades.Context
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options) { }
        public virtual DbSet<Usuario> Usuario { get; set; }

        public virtual DbSet<PermisosUsuario> Permisos { get; set; }
    }
}

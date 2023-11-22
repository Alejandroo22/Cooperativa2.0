using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.UserData
{
    class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public string ClaveUsuario { get; set; }
        public string EstadoUsuario { get; set; }
        public DateTime FechaCreacionUsuario { get; set; }
        public DateTime? FechaBajaUsuario { get; set; }
        public int IdRolUsuario { get; set; }
        public string NombreRol { get; set; }
        public int IdPersonaUsuario { get; set; }
        public string NombrePersonaUsuario { get; set; }
        public string ApellidoPersonaUsuario { get; set; }
    }

    public static class UsuarioActual
    {
        public static int IUsuario { get; set; }
        public static string NombreUsuario { get; set; }
        public static int RolUsuario { get; set; }
    }

    public static class UsuarioSeleccionado
    {
        public static string Usuario { get; set; }
    }
}
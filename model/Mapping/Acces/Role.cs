using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.Acces
{
    class Role
    {
        public int IdRol { get; set; }
        public string NombreRol { get; set; }
        public string DescripcionRol {get ; set; }
        public string NivelAccesoRol {get ; set; }
        public string PermisosRol {get ; set; }
        public DateTime FechaCreacionRol {get ; set; }
        public DateTime UltFechaModRol {get ; set; }
    }
}

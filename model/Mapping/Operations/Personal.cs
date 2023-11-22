using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.Mapping.Operations
{
    class Personal
    {
        public int IdPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public int ICargo { get; set; }
        public string NombreCargo { get; set; }
        public string Descripcion { get; set; }
        public int IdPersona { get; set; }
        public string NombrePersona { get; set; }

    }

    public static class PersonalSeleccionado
    {
        public static int IPersonal { get; set; }
        public static string TipoPersonal { get; set; }
        public static string NombrePersonal { get; set; }
        public static int IPersonalCatador { get; set; }
        public static string NombrePersonalCatador { get; set; }
        public static int IPersonalPuntero { get; set; }
        public static string NombrePersonalPuntero { get; set; }
        public static int IPersonalPesador { get; set; }
        public static string NombrePersonalPesador { get; set; }
    }
}

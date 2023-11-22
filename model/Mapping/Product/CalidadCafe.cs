using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.Mapping
{
    class CalidadCafe
    {
        public int IdCalidad { get; set; }
        public string NombreCalidad { get; set; }
        public string DescripcionCalidad { get; set;}
        public int CountCalidad { get; set;}
        public int LastId { get; set;}

    }

    public static class CalidadSeleccionada
    {
        public static int ICalidadSeleccionada { get; set; }
        public static string NombreCalidadSeleccionada { get; set; }
    }
}

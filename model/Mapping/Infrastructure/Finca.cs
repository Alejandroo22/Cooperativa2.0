using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.Mapping
{
    class Finca
    {
        public int IdFinca { get; set; }
        public string nombreFinca { get; set; }
        public string ubicacionFinca { get; set; }
        public int CountFinca { get; set; }
        public int LastId { get; set; }
    }

    public static class FincaSeleccionada
    {
        public static int IFincaSeleccionada { get; set; }
        public static string NombreFincaSeleccionada { get; set; }
    }
}

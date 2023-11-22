using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.Mapping.Product
{
    class TipoCafe
    {
        public int IdTipoCafe { get; set; }
        public string NombreTipoCafe { get; set; }
        public string DescripcionTipoCafe { get; set; }
        public int CountTipoCafe { get; set; }
        public int LastId { get; set; }
    }

    public static class TipoCafeSeleccionado
    {
        public static int ITipoCafeSeleccionado { get; set; }
        public static string NombreTipoCafeSeleccionado { get; set; }
    }
}

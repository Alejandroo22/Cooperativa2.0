using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.Mapping.Infrastructure
{
    class Beneficio
    {
        public int IdBeneficio { get; set; }
        public string NombreBeneficio { get; set; }
        public string UbicacionBeneficio { get; set; }
        public int CountBeneficio { get; set; }
        public int LastId { get; set; }
    }

    public static class BeneficioSeleccionado
    {
        public static int IdBeneficioSleccionado { get; set; }
        public static string NombreBeneficioSeleccionado { get; set; }
    }
}

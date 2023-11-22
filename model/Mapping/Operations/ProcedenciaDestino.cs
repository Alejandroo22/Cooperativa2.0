using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.Mapping.Operations
{
    class ProcedenciaDestino
    {
        public int IdProcedencia { get; set; }
        public string NombreProcedencia { get; set; }
        public string DescripcionProcedencia { get; set; }
        public int IdBenficioUbicacion { get; set; }
        public string NombreBenficioUbicacion { get; set; }
        public int IdSocioProcedencia { get; set; }
        public string NombreSocioProcedencia { get; set; }
        public string NombreFincaSocio { get; set; }
        public int IdMaquinaria { get; set; }
        public string NombreMaquinaria { get; set; }
        public int CountProcedencia { get; set; }
        public int LastId { get; set; }
    }

    public static class ProcedenciaSeleccionada
    {
        public static int IProcedencia { get; set; }
        public static string NombreProcedencia { get; set; }
        public static int IProcedenciaDestino { get; set; }
        public static string NombreProcedenciaDestino { get; set; }
    }
}

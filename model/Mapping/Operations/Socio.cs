using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.Mapping.Operations
{
    class Socio
    {
        public int IdSocio { get; set; }
        public string NombreSocio { get; set; }
        public string DescripcionSocio { get; set; }
        public string UbicacionSocio { get; set; }
        public int IdPersonaRespSocio { get; set; }
        public string NombrePersonaResp { get; set; }
        public int IdFincaSocio { get; set; }
        public string NombreFinca { get; set; }
        public int CountSocio { get; set; }
        public int LastId { get; set; }
    }

    public static class TablaSeleccionada
    {
        public static int ITable { get; set; }
    }
}

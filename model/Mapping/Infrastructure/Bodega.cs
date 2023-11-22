using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.Mapping.Infrastructure
{
    class Bodega
    {
        public int IdBodega { get; set; }
        public string NombreBodega { get; set; }
        public string DescripcionBodega { get; set; }
        public string UbicacionBodega { get; set; }
        public int IdBenficioUbicacion { get; set; }
        public string NombreBenficioUbicacion { get; set; }
        public int CountBodega { get; set; }
        public int LastId { get; set; }
    }

    public static class BodegaSeleccionada
    {
        public static int IdBodega { get; set; }
        public static string NombreBodega { get; set; }
        public static int IdBodegaDestino { get; set; }
        public static string NombreBodegaDestino { get; set; }
    }

    public static class TablaSeleccionadabodega
    {
        public static int ITable { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema_modular_cafe_majada.model.Mapping.Harvest
{
    class Lote
    {
        public int IdLote { get; set; }
        public string NombreLote { get; set; }
        public DateTime FechaLote { get; set; }
        public double CantidadLote { get; set; }
        public int IdTipoCafe { get; set; }
        public string TipoCafe { get; set; }
        public int IdFinca { get; set; }
        public string NombreFinca { get; set; }
        public int IdCalidadLote { get; set; }
        public string NombreCalidadLote { get; set; }
        public int IdCosechaLote { get; set; }
        public string NombreCosechaLote { get; set; }
    }

    public static class TablaSeleccionada
    {
        public static int ITable { get; set; }
    }
}
